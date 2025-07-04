using System;
using MediatR;
using QNF.Plataforma.Application.DTOs;

namespace QNF.Plataforma.Application.Queries;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserResponse>;