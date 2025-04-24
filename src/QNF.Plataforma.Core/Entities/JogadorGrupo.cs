namespace QNF.Plataforma.Core.Entities;

public class JogadorGrupo : BaseEntity
{
    public Guid JogadorId { get; private set; }
    public Guid GrupoId { get; private set; }
    public DateTime DataEntrada { get; private set; }
    public bool Ativo { get; private set; }

    private JogadorGrupo() { }

    public JogadorGrupo(Guid jogadorId, Guid grupoId)
    {
        JogadorId = jogadorId;
        GrupoId = grupoId;
        DataEntrada = DateTime.UtcNow;
        Ativo = true;
    }

    public void Inativar() => Ativo = false;
}