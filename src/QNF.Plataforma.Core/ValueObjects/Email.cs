using System.Text.RegularExpressions;

namespace QNF.Plataforma.Core.ValueObjects;

public class Email : IEquatable<Email>
{
    private static readonly Regex EmailRegex = 
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    public string Value { get; private set; } = null!;

    protected Email() { } 

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("O e-mail é obrigatório.", nameof(value));

        if (!EmailRegex.IsMatch(value))
            throw new ArgumentException("O e-mail informado é inválido.", nameof(value));

        Value = value.ToLowerInvariant();
    }

    public bool Equals(Email? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Email other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;
    public static implicit operator string(Email email) => email.Value;
}
