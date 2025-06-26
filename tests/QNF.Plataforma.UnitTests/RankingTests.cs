namespace QNF.Plataforma.UnitTests;

using QNF.Plataforma.Core.Entities;

public class RankingTests
{
    [Fact]
    public void RegistrarVitoriaOuDerrota_AtualizaEstatisticasEAproveitamento()
    {
        var grupoId = Guid.NewGuid();
        var jogadorId = Guid.NewGuid();
        var ranking = new Ranking(grupoId, jogadorId);

        ranking.RegistrarVitoria();
        ranking.RegistrarDerrota();

        Assert.Equal(2, ranking.Jogos);
        Assert.Equal(1, ranking.Vitorias);
        Assert.Equal(1, ranking.Derrotas);
        Assert.Equal(50d, ranking.Aproveitamento);
    }
}
