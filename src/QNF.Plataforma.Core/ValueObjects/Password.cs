using QNF.Plataforma.Application.Interfaces;

namespace QNF.Plataforma.Core.ValueObjects;

public class Password : IEquatable<Password>
{
    public string HashedValue { get; private set; } = null!;

    protected Password() { } // Para ORM

    public Password(string hashedValue)
    {
        if (string.IsNullOrWhiteSpace(hashedValue))
            throw new ArgumentException("O hash da senha é obrigatório.", nameof(hashedValue));

        HashedValue = hashedValue;
    }

    public bool Equals(Password? other) =>
        other is not null && HashedValue == other.HashedValue;

    public override bool Equals(object? obj) => obj is Password other && Equals(other);
    public override int GetHashCode() => HashedValue.GetHashCode();
    public override string ToString() => "********";
}