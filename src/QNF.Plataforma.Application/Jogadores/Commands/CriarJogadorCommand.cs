namespace QNF.Plataforma.Application.Jogadores.Commands;

public record CriarJogadorCommand(string Nome, string? Apelido, string? Telefone, string? Email);