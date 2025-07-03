using MediatR;

namespace MyApp.Application.Commands
{
    public class UpdateGameCommand : IRequest
    {
        public Guid Id { get; }
        public string RightPlayerTeamA { get; }
        public string LeftPlayerTeamA { get; }
        public int PointsTeamA { get; }
        public string RightPlayerTeamB { get; }
        public string LeftPlayerTeamB { get; }
        public int PointsTeamB { get; }

        public UpdateGameCommand(
            Guid id,
            string rightPlayerTeamA,
            string leftPlayerTeamA,
            int pointsTeamA,
            string rightPlayerTeamB,
            string leftPlayerTeamB,
            int pointsTeamB)
        {
            Id = id;
            RightPlayerTeamA = rightPlayerTeamA;
            LeftPlayerTeamA = leftPlayerTeamA;
            PointsTeamA = pointsTeamA;
            RightPlayerTeamB = rightPlayerTeamB;
            LeftPlayerTeamB = leftPlayerTeamB;
            PointsTeamB = pointsTeamB;
        }
    }
}
