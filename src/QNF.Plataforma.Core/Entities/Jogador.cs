namespace QNF.Plataforma.Core.Entities;

public class Jogador : BaseEntity
{
    public Guid UsuarioId { get; set; }
    public string? Nome { get; set; }
    public string? Apelido { get; set; }
    public string? FotoPerfilUrl { get; set; }
    public string? Telefone { get; set; }
    public string Email { get; set; }
    public string? TamanhoCamiseta { get; set; }
    public DateTime? DataNascimento { get; set; }

    public Jogador() { }

    public Jogador(Guid usuarioId, string nome, string? apelido = null, string? telefone = null, string? email = null)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Apelido = apelido;
        Telefone = telefone;
        Email = email;
    }

    public void AtualizarNome(string? nome) => Nome = nome?.Trim();
    public void AtualizarFoto(string url) => FotoPerfilUrl = url;
    public void AtualizarTamanhoCamiseta(string tamanho) => TamanhoCamiseta = tamanho;
}