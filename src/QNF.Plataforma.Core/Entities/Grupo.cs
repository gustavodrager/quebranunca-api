namespace QNF.Plataforma.Core.Entities;

public class Grupo : BaseEntity
{
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public Guid CriadoPorJogadorId { get; private set; }

    private readonly List<Guid> _membros = new();
    public IReadOnlyCollection<Guid> Membros => _membros.AsReadOnly();

    private Grupo() { }

    public Grupo(string nome, Guid criadoPorJogadorId, string? descricao = null)
    {
        Nome = nome;
        Descricao = descricao;
        CriadoPorJogadorId = criadoPorJogadorId;
    }

    public void AdicionarJogador(Guid jogadorId)
    {
        if (!_membros.Contains(jogadorId))
            _membros.Add(jogadorId);
    }

    public void RemoverJogador(Guid jogadorId)
    {
        _membros.Remove(jogadorId);
    }
}