namespace QNF.Plataforma.Application.DTOs;

public record ResetPasswordRequest(string Email, string Token, string NewPassword);