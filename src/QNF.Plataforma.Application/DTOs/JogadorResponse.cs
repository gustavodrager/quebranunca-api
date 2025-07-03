namespace QNF.Plataforma.Application.DTOs;

public class JogadorResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Apelido { get; set; }
}