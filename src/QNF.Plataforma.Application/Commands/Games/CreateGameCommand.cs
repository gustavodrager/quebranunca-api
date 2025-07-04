using MediatR;

namespace MyApp.Application.Commands;

public class CreateGameCommand : IRequest<Guid>
{
    public string RightPlayerTeamA { get; }
    public string LeftPlayerTeamA { get; }
    public int PointsTeamA { get; }
    public string RightPlayerTeamB { get; }
    public string LeftPlayerTeamB { get; }
    public int PointsTeamB { get; }

    public CreateGameCommand(
        string rightPlayerTeamA,
        string leftPlayerTeamA,
        int pointsTeamA,
        string rightPlayerTeamB,
        string leftPlayerTeamB,
        int pointsTeamB)
    {
        RightPlayerTeamA = rightPlayerTeamA;
        LeftPlayerTeamA = leftPlayerTeamA;
        PointsTeamA = pointsTeamA;
        RightPlayerTeamB = rightPlayerTeamB;
        LeftPlayerTeamB = leftPlayerTeamB;
        PointsTeamB = pointsTeamB;
    }
}