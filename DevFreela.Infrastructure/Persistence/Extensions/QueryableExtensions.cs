using DevFreela.Application.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace DevFreela.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    // Paginação assíncrona
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query, PaginationParameters parameters)
    {
        // Contamos o total de itens da query
        var count = await query.CountAsync();

        // Pulamos os N primeiros itens (conforme a página) e pegamos a quantidade definida em PageSize
        var items = await query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                .Take(parameters.PageSize)
                                .ToListAsync();

        var pagedResult = new PagedResult<T>
        {
            CurrentPage = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)parameters.PageSize),
            Items = items
        };

        return pagedResult;
    }

    // Ordenação dinâmica
    public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string orderBy, bool ascending = true)
    {
        // Valida se tem algum parâmetro para validação, se não tiver, retorna a query inicial
        if (string.IsNullOrWhiteSpace(orderBy))
            return query;

        // Obtém as propriedades públicas de T (tipo) 
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Verifica se a propriedade passada existe 
        var propertyInfo = properties.FirstOrDefault(p => p.Name.Equals(orderBy, StringComparison.OrdinalIgnoreCase));

        // Se a propriedade não existir, retorna a consulta sem ordenação
        if (propertyInfo == null)
            return query;

        // Construção da expressão lambda para ordenação
        var parameter = Expression.Parameter(typeof(T), "x"); // Cria um parâmetro x do tipo T
        var property = Expression.Property(parameter, propertyInfo); // Acessa a propriedade x.Property
        var lambda = Expression.Lambda(property, parameter); // Cria expressão lambda x => x.Property

        // Define o método de ordenação (asc, desc)
        var methodName = ascending ? "OrderBy" : "OrderByDescending";

        // Cria uma chamada ao método Queryable.OrderBy[Descending]
        var methodCallExpression = Expression.Call(
            typeof(Queryable), // Método de Queryable
            methodName, // OrderBy[Descending]
            [typeof(T), propertyInfo.PropertyType], // Especifica os tipos genéricos (T e tipo da propriedade)
            query.Expression,
            Expression.Quote(lambda)); // Passa a expressão original e a lambda

        // Aplica a expressão ao provedor LINQ
        return query.Provider.CreateQuery<T>(methodCallExpression);
    }

    // Método de extensão para filtro baseado em uma propriedade e valor
    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, string propertyName, string filterValue)
    {
        // Valida se a propriedade ou o valor é vazio. Se sim, retorna a query inicial
        if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filterValue))
            return query;


        // Obtém as propriedades públicas de T (tipo) 
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Verifica se a propriedade passada existe 
        var propertyInfo = properties.FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

        // Se a propriedade não existir, retorna a consulta sem filtragem
        if (propertyInfo == null)
            return query;

        // Construção da expressão lambda para filtragem
        var parameter = Expression.Parameter(typeof(T), "x"); // Cria um parâmetro x do tipo T
        var property = Expression.Property(parameter, propertyInfo); // Acessa a propriedade x.Property

        Expression filterExpression;

        // Dependendo do tipo da propriedade, construímos expressões diferentes
        if (propertyInfo.PropertyType == typeof(string))
        {
            var constant = Expression.Constant(filterValue, typeof(string));

            // Verifica se a string contém o valor de filtro (case insensitive)
            var method = typeof(string).GetMethod("Contains", [typeof(string)]);

            filterExpression = Expression.Call(property, method!, constant);
        }
        else if (propertyInfo.PropertyType == typeof(int) && int.TryParse(filterValue, out int intValue))
        {
            var constant = Expression.Constant(intValue, typeof(int));
            filterExpression = Expression.Equal(property, constant);
        }
        else if (propertyInfo.PropertyType == typeof(DateTime) && DateTime.TryParse(filterValue, out DateTime dateValue))
        {
            var constant = Expression.Constant(dateValue, typeof(DateTime));
            filterExpression = Expression.Equal(property, constant);
        }
        else if (propertyInfo.PropertyType == typeof(bool) && bool.TryParse(filterValue, out bool boolValue))
        {
            var constant = Expression.Constant(boolValue, typeof(bool));
            filterExpression = Expression.Equal(property, constant);
        }
        else
        {
            // Se não conseguirmos construir um filtro válido, retornamos a consulta original
            return query;
        }

        // Criando a expressão lambda completa
        var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);

        // Aplicando o filtro usando Where
        return query.Where(lambda);
    }

    // Método de extensão para busca em várias propriedades de texto
    public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, string searchTerm, params string[] propertyNames)
    {
        // Valida se o método de busca e as propriedades existem, caso não, retorna a query inicial
        if (string.IsNullOrEmpty(searchTerm) || propertyNames.Length == 0)
            return query;

        // Obtém as propriedades do tipo T públicas e de instância
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Cria um parâmetro para expressão lambda
        var parameter = Expression.Parameter(typeof(T), "x");

        // Iniciamos com uma expressão "false" para construir as OR conditions
        Expression combinedExpression = Expression.Constant(false);

        // Método Contains da classe string
        var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
        var searchConstant = Expression.Constant(searchTerm, typeof(string));

        foreach (var propName in propertyNames)
        {
            // Verifica se a propriedade existe e é do tipo string
            var propertyInfo = properties.FirstOrDefault(p =>
                p.Name.Equals(propName, StringComparison.OrdinalIgnoreCase) &&
                p.PropertyType == typeof(string));

            if (propertyInfo != null)
            {
                // Cria uma expressão para acessar a propriedade do objeto
                var property = Expression.Property(parameter, propertyInfo);

                // Verifica se a propriedade não é null antes de chamar Contains
                var notNullCheck = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));

                // Cria a chamada para o método Contains
                var containsCall = Expression.Call(property, containsMethod!, searchConstant);

                // Combina a verificação de null com a chamada de Contains
                var safeContainsCall = Expression.AndAlso(notNullCheck, containsCall);

                // Adiciona esta condição ao combinedExpression usando OR (||)
                combinedExpression = Expression.OrElse(combinedExpression, safeContainsCall);
            }
        }
        // Cria a expressão lambda final
        var lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);

        // Aplica o filtro
        return query.Where(lambda);
    }

    // Método de extensão que aplica paginação, ordenação e filtragem de uma vez
    public static IQueryable<T> ApplyQueryParameters<T>(this IQueryable<T> query, QueryParameters parameters, string[] searchableProperties = null!)
    {
        // Aplica o filtro de busca, se houver
        if (!string.IsNullOrWhiteSpace(parameters.SearchTerm) && searchableProperties != null && searchableProperties.Length > 0)
        {
            query = query.ApplySearch(parameters.SearchTerm, searchableProperties);
        }

        // Aplica o filtro específico, se houver
        if (!string.IsNullOrWhiteSpace(parameters.FilterField) && !string.IsNullOrWhiteSpace(parameters.FilterValue))
        {
            query = query.ApplyFilter(parameters.FilterField, parameters.FilterValue);
        }

        // Aplica a ordenação, se houver
        if (!string.IsNullOrWhiteSpace(parameters.SortBy))
        {
            query = query.ApplySort(parameters.SortBy, parameters.Ascending);
        }

        return query;
    }
}