namespace QNF.Plataforma.Application.Grupos.Commands;

public record AdicionarJogadorAoGrupoCommand(Guid GrupoId, Guid JogadorId);