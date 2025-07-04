using MediatR;
using MyApp.Application.Commands;
using QNF.Plataforma.Application.Events;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Entities;
using MassTransit;

namespace MyApp.Application.Handlers
{
    public class CreateGameHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IGameRepository _repository;
        private readonly IPublishEndpoint _publisher;

        public CreateGameHandler(IGameRepository repository, IPublishEndpoint publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken ct)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                RightPlayerTeamA = request.RightPlayerTeamA,
                LeftPlayerTeamA = request.LeftPlayerTeamA,
                PointsTeamA = request.PointsTeamA,
                RightPlayerTeamB = request.RightPlayerTeamB,
                LeftPlayerTeamB = request.LeftPlayerTeamB,
                PointsTeamB = request.PointsTeamB
            };

            await _repository.AddAsync(game);
            
            await _publisher.Publish(new GameCreatedEvent(
                game.Id,
                game.RightPlayerTeamA,
                game.LeftPlayerTeamA,
                game.PointsTeamA,
                game.RightPlayerTeamB,
                game.LeftPlayerTeamB,
                game.PointsTeamB
            ));

            return game.Id;
        }
    }
}