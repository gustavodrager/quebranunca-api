namespace QNF.Plataforma.Application.DTOs;

public class UltimoJogoResponse
{
    public Guid Dupla1Id { get; set; }
    public Guid Dupla2Id { get; set; }
    public int PontuacaoDupla1 { get; set; }
    public int PontuacaoDupla2 { get; set; }
    public DateTime DataHora { get; set; }
}
