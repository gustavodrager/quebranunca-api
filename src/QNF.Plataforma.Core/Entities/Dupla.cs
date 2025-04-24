namespace QNF.Plataforma.Core.Entities;

public class Dupla : BaseEntity
{
    public Guid Jogador1Id { get; private set; }
    public Guid Jogador2Id { get; private set; }
    public DateTime DataCriacao { get; private set; }

    private Dupla() { }

    public Dupla(Guid jogador1Id, Guid jogador2Id)
    {
        if (jogador1Id == jogador2Id)
            throw new ArgumentException("Uma dupla precisa ter jogadores diferentes.");

        Jogador1Id = jogador1Id;
        Jogador2Id = jogador2Id;
        DataCriacao = DateTime.UtcNow;
    }
}