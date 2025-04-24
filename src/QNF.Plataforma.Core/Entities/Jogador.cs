namespace QNF.Plataforma.Core.Entities;

public class Jogador : BaseEntity
{
    public string Nome { get; private set; }
    public string? Apelido { get; private set; }
    public string? FotoPerfilUrl { get; private set; }
    public string? Telefone { get; private set; }
    public string? Email { get; private set; }
    public string? TamanhoCamiseta { get; private set; }
    public DateTime? DataNascimento { get; private set; }

    private Jogador() { }

    public Jogador(string nome, string? apelido = null, string? telefone = null, string? email = null)
    {
        Nome = nome;
        Apelido = apelido;
        Telefone = telefone;
        Email = email;
    }

    public void AtualizarFoto(string url) => FotoPerfilUrl = url;
    public void AtualizarTamanhoCamiseta(string tamanho) => TamanhoCamiseta = tamanho;
}