namespace QNF.Plataforma.UnitTests;

using QNF.Plataforma.Core.Entities;

public class GrupoTests
{
    [Fact]
    public void AdicionarJogador_NaoPermiteJogadorDuplicado()
    {
        var criadorId = Guid.NewGuid();
        var grupo = new Grupo("Treinos", criadorId);
        var jogadorId = Guid.NewGuid();

        grupo.AdicionarJogador(jogadorId);
        grupo.AdicionarJogador(jogadorId);

        Assert.Single(grupo.Membros);
    }

    [Fact]
    public void RemoverJogador_DeixaGrupoSemMembro()
    {
        var criadorId = Guid.NewGuid();
        var grupo = new Grupo("Treinos", criadorId);
        var jogadorId = Guid.NewGuid();

        grupo.AdicionarJogador(jogadorId);
        grupo.RemoverJogador(jogadorId);

        Assert.Empty(grupo.Membros);
    }
}
