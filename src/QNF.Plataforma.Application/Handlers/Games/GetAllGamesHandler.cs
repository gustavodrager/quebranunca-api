using MediatR;
using QNF.Application.DTOs;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Application.Queries;

namespace QNF.Plataforma.Application.Handlers;

public class GetAllGamesHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<GameDto>>
    {
        private readonly IGameRepository _repository;

        public GetAllGamesHandler(IGameRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GameDto>> Handle(GetAllGamesQuery request, CancellationToken ct)
        {
            var games = await _repository.GetAllAsync();
            var dtos = games.Select(game => new GameDto
            {
                Id = game.Id,
                RightPlayerTeamA = game.RightPlayerTeamA,
                LeftPlayerTeamA = game.LeftPlayerTeamA,
                PointsTeamA = game.PointsTeamA,
                RightPlayerTeamB = game.RightPlayerTeamB,
                LeftPlayerTeamB = game.LeftPlayerTeamB,
                PointsTeamB = game.PointsTeamB
            });
            return dtos;
        }
    }