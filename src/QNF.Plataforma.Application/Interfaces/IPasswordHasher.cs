using System;

namespace QNF.Plataforma.Application.Interfaces;

public interface IPasswordHasher
{
    /// <summary>
    /// Gera o hash de uma senha em texto puro.
    /// </summary>
    string Hash(string plainTextPassword);

    /// <summary>
    /// Verifica se a senha em texto puro corresponde ao hash armazenado.
    /// </summary>
    bool Verify(string hashedPassword, string plainTextPassword);
}
