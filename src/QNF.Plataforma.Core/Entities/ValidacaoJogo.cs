namespace QNF.Plataforma.Core.Entities;

public class ValidacaoJogo : BaseEntity
{
    public Guid JogoId { get; private set; }
    public Guid JogadorId { get; private set; }
    public DateTime DataValidacao { get; private set; }
    public ValidacaoStatus Status { get; private set; }
    public string? Comentario { get; private set; }

    private ValidacaoJogo() { }

    public ValidacaoJogo(Guid jogoId, Guid jogadorId, ValidacaoStatus status, string? comentario = null)
    {
        JogoId = jogoId;
        JogadorId = jogadorId;
        Status = status;
        Comentario = comentario;
        DataValidacao = DateTime.UtcNow;
    }

    public void AtualizarStatus(ValidacaoStatus status, string? comentario = null)
    {
        Status = status;
        Comentario = comentario;
        DataValidacao = DateTime.UtcNow;
    }
}

public enum ValidacaoStatus
{
    Aprovado = 1,
    Reprovado = 2
}