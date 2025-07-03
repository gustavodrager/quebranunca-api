namespace QNF.Plataforma.Application.Events;

public record GameCreatedEvent(
    Guid Id,
    string RightPlayerTeamA,
    string LeftPlayerTeamA,
    int PointsTeamA,
    string RightPlayerTeamB,
    string LeftPlayerTeamB,
    int PointsTeamB
);