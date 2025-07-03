namespace QNF.Plataforma.API.Requests;

public class RegistrarJogoRequest { 
    public string GrupoNome { get; set; } = string.Empty;
    public string JogadorDireita { get; set; } = string.Empty;
    public string TimeB { get; set; } = string.Empty;
    public int PlacarA { get; set; }
    public int PlacarB { get; set; }
}