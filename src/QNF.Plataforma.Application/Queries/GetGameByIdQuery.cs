using MediatR;
using QNF.Application.DTOs;

namespace QNF.Plataforma.Application.Queries;

public class GetGameByIdQuery : IRequest<GameDto>
{
    public Guid Id { get; }

    public GetGameByIdQuery(Guid id)
    {
        Id = id;
    }
}