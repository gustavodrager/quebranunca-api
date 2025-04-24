namespace QNF.Plataforma.Application.Grupos.Commands;

public record CriarGrupoCommand(string Nome, Guid CriadoPorJogadorId, string? Descricao);