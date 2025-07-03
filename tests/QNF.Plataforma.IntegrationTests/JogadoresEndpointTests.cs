using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace QNF.Plataforma.IntegrationTests;

public class JogadoresEndpointTests
{
    [Fact]
    public async Task CriarJogador_RetornaId()
    {
        await using var factory = new ApiFactory();
        using var client = factory.CreateClient();

        var request = new
        {
            Nome = "Jogador Integracao",
            Apelido = "Teste",
            Telefone = "12345",
            Email = "int@test.com"
        };

        var response = await client.PostAsJsonAsync("/api/jogadores", request);

        response.EnsureSuccessStatusCode();
        var doc = await response.Content.ReadFromJsonAsync<JsonElement>();
        var id = doc.GetProperty("id").GetGuid();

        Assert.NotEqual(Guid.Empty, id);
    }
}
