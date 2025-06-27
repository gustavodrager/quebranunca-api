namespace QNF.Plataforma.Application.DTOs;

public class RankingResponse
{
    public string NomeJogador { get; set; } = default!;
    public int Jogos { get; set; }
    public int Vitorias { get; set; }
    public int Derrotas { get; set; }
    public double Aproveitamento { get; set; }
}
