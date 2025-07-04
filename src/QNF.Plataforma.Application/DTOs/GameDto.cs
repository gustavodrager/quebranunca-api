namespace QNF.Application.DTOs;

public class GameDto
{
    public Guid Id { get; set; }
    public required string RightPlayerTeamA { get; set; }
    public required string LeftPlayerTeamA { get; set; }
    public required int PointsTeamA { get; set; }
    public required string RightPlayerTeamB { get; set; }
    public required string LeftPlayerTeamB { get; set; }
    public int PointsTeamB { get; set; }
}