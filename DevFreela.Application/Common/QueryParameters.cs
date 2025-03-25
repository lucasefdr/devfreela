namespace DevFreela.Application.Common;

// Classe para entrada de parâmetros no controller
public class QueryParameters : PaginationParameters
{
    // Ordenação
    public string SortBy { get; set; } = string.Empty;
    public bool Ascending { get; set; } = true;

    // Filtragem dinâmica
    public string SearchTerm { get; set; } = string.Empty;
    public string FilterField { get; set; } = string.Empty;
    public string FilterValue { get; set; } = string.Empty;
}
