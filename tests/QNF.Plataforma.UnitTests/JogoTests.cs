namespace QNF.Plataforma.UnitTests;

using QNF.Plataforma.Core.Entities;

public class JogoTests
{
    [Fact]
    public void Constructor_WithSameDuplaIds_ThrowsArgumentException()
    {
        var grupoId = Guid.NewGuid();
        var duplaId = Guid.NewGuid();
        var criadorId = Guid.NewGuid();
        var dataHora = DateTime.UtcNow;

        Assert.Throws<ArgumentException>(() =>
            new Jogo(grupoId, duplaId, duplaId, criadorId, dataHora, "Quadra"));
    }

    [Fact]
    public void RegistrarResultado_UpdatesScoresAndStatus()
    {
        var grupoId = Guid.NewGuid();
        var dupla1Id = Guid.NewGuid();
        var dupla2Id = Guid.NewGuid();
        var criadorId = Guid.NewGuid();
        var jogo = new Jogo(grupoId, dupla1Id, dupla2Id, criadorId, DateTime.UtcNow, null);

        jogo.RegistrarResultado(21, 18);

        Assert.Equal(21, jogo.PontuacaoDupla1);
        Assert.Equal(18, jogo.PontuacaoDupla2);
        Assert.Equal(JogoStatus.AguardandoValidacao, jogo.Status);
    }

    [Fact]
    public void Aprovar_ChangesStatusToAprovado()
    {
        var grupoId = Guid.NewGuid();
        var dupla1Id = Guid.NewGuid();
        var dupla2Id = Guid.NewGuid();
        var criadorId = Guid.NewGuid();
        var jogo = new Jogo(grupoId, dupla1Id, dupla2Id, criadorId, DateTime.UtcNow, null);

        jogo.Aprovar();

        Assert.Equal(JogoStatus.Aprovado, jogo.Status);
    }

    [Fact]
    public void Rejeitar_ChangesStatusToRejeitado()
    {
        var grupoId = Guid.NewGuid();
        var dupla1Id = Guid.NewGuid();
        var dupla2Id = Guid.NewGuid();
        var criadorId = Guid.NewGuid();
        var jogo = new Jogo(grupoId, dupla1Id, dupla2Id, criadorId, DateTime.UtcNow, null);

        jogo.Rejeitar();

        Assert.Equal(JogoStatus.Rejeitado, jogo.Status);
    }
}
