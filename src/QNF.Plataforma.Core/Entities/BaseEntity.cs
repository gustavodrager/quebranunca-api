namespace QNF.Plataforma.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; private set; }

    public Guid? CriadoPor { get; private set; }
    public Guid? AtualizadoPor { get; private set; }

    public void MarcarAtualizacao(Guid usuarioId)
    {
        DataAtualizacao = DateTime.UtcNow;
        AtualizadoPor = usuarioId;
    }

    public void MarcarCriacao(Guid usuarioId)
    {
        CriadoPor = usuarioId;
    }
}