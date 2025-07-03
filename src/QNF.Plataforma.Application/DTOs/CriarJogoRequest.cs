namespace QNF.Plataforma.Application.DTOs;

public class CriarJogoRequest
{
    public string GrupoNome { get; set; } = string.Empty;
    public List<string> TimeA { get; set; } = [];
    public List<string> TimeB { get; set; } = [];
    public int PlacarA { get; set; }
    public int PlacarB { get; set; }
}