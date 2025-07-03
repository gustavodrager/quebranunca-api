using MassTransit;
using QNF.Plataforma.Application.Events;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Infrastructure.Data;

namespace QNF.Plataforma.Infrastructure.Consumers;

public class GameCreatedConsumer : IConsumer<GameCreatedEvent>
{
    private readonly ReadDbContext _read;
    public GameCreatedConsumer(ReadDbContext read) => _read = read;

    public async Task Consume(ConsumeContext<GameCreatedEvent> ctx)
    {
        var e = ctx.Message;
        _read.Games.Add(new Game {
            Id                = e.Id,
            RightPlayerTeamA  = e.RightPlayerTeamA,
            LeftPlayerTeamA   = e.LeftPlayerTeamA,
            PointsTeamA       = e.PointsTeamA,
            RightPlayerTeamB  = e.RightPlayerTeamB,
            LeftPlayerTeamB   = e.LeftPlayerTeamB,
            PointsTeamB       = e.PointsTeamB
        });
        await _read.SaveChangesAsync(ctx.CancellationToken);
    }
}