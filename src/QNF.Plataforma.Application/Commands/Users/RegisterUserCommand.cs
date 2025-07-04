using System;
using MediatR;
using QNF.Plataforma.Application.DTOs;

namespace QNF.Plataforma.Application.Commands.User;

public record RegisterUserCommand(RegisterUserRequest registerUserRequest) : IRequest<RegisterUserResponse>;