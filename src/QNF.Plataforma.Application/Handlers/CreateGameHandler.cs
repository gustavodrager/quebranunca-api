using MediatR;
using MyApp.Application.Commands;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Entities;

namespace MyApp.Application.Handlers
{
    public class CreateGameHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IGameRepository _repository;

        public CreateGameHandler(IGameRepository repository)
        {
            _repository = repository;
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

            // Persist via repository
            await _repository.AddAsync(game);
            return game.Id;
        }
    }
}