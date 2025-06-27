public class CriarJogadorRequest
{
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Apelido { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? TamanhoCamiseta { get; set; }
    public DateTime? DataNascimento { get; set; }
}