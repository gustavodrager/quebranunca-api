using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.API.Requests;

public class ValidarJogoRequest
{
    public Guid JogadorId { get; set; }
    public ValidacaoStatus Status { get; set; }
    public string? Comentario { get; set; }
}