namespace QNF.Plataforma.Core.Entities;

public class Game 
{
    public Guid Id { get; set; }
    public required string RightPlayerTeamA { get; set; }
    public required string LeftPlayerTeamA { get; set; }
    public required int PointsTeamA { get; set; }
    public required string RightPlayerTeamB { get; set; }
    public required string LeftPlayerTeamB { get; set; }
    public required int PointsTeamB { get; set; }
}