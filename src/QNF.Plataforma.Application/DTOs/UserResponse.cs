using System;

namespace QNF.Plataforma.Application.DTOs;

public class UserResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}
