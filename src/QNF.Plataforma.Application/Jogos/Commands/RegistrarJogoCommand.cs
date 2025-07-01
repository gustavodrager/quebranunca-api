namespace QNF.Plataforma.Application.Jogos.Commands;

public record RegistrarJogoCommand(
    string NomeGrupo,
    Guid Dupla1Id,
    Guid Dupla2Id,
    Guid CriadoPorJogadorId,
    DateTime DataHora,
    string? Local,
    int PontuacaoDupla1,
    int PontuacaoDupla2
);