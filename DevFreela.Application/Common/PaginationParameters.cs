namespace DevFreela.Application.Common;

// Classe que representa a entrada de parâmetros
public class PaginationParameters
{
    private const int MaxPageSize = 50; // Tamanho máximo da página
    private int _pageSize = 10; // Tamanho padrão da página

    public int PageNumber { get; set; } = 1; // Página inicial

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value; // Se for solicitado mais de MaxPageSize, não permite
    }
}
