namespace QNF.Plataforma.Core.Entities;

public class Dupla : BaseEntity
{
    public Guid Jogador1Id { get; set; }
    public Guid Jogador2Id { get; set; }
    public Jogador? Jogador1 { get; set; } = null!;
    public Jogador? Jogador2 { get; set; } = null!;
    public IEnumerable<Jogador> Jogadores => new[] { Jogador1, Jogador2 };

    public Dupla() { }

    public Dupla(Guid jogador1Id, Guid jogador2Id)
    {
        if (jogador1Id == jogador2Id)
            throw new ArgumentException("Uma dupla precisa ter jogadores diferentes.");

        Jogador1Id = jogador1Id;
        Jogador2Id = jogador2Id;
    }
}