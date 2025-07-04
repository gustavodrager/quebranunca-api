using System;

namespace QNF.Plataforma.Application.DTOs;

public class RegisterUserResponse
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}
