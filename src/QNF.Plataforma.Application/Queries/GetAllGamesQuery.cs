using MediatR;
using QNF.Application.DTOs;

namespace QNF.Plataforma.Application.Queries;

 public class GetAllGamesQuery : IRequest<IEnumerable<GameDto>> { }
