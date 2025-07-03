namespace QNF.Application.DTOs;

public class GameDto
{
    public Guid Id { get; set; }
    public string RightPlayerTeamA { get; set; }
    public string LeftPlayerTeamA { get; set; }
    public int PointsTeamA { get; set; }
    public string RightPlayerTeamB { get; set; }
    public string LeftPlayerTeamB { get; set; }
    public int PointsTeamB { get; set; }
}