namespace DevFreela.Application.Common;

// Classe de paginação genérica
public class PagedResult<T>
{
    public int CurrentPage { get; set; } // Página atual
    public int PageSize { get; set; } // Tamanho da página
    public int TotalCount { get; set; } // Total de resultados
    public int TotalPages { get; set; } // Total de páginas
    public bool HasPrevious => CurrentPage > 1; // Verifica se tem página anterior
    public bool HasNext => CurrentPage < TotalPages; // Verifica se tem próxima página
    public List<T> Items { get; set; } // Itens retornados

    public PagedResult()
    {
        Items = [];
    }
}
