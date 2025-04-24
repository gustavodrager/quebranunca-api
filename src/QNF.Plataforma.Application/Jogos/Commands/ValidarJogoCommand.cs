using QNF.Plataforma.Core.Entities;

namespace QNF.Plataforma.Application.Jogos.Commands;

public record ValidarJogoCommand(
    Guid JogoId,
    Guid JogadorId,
    ValidacaoStatus Status,
    string? Comentario
);