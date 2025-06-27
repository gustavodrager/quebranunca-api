using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Services;

public class JogadorService : IJogadorService
{
    private readonly IJogadorRepository _repo;
    private readonly IUnitOfWork _unitOfWork;

    public JogadorService(IJogadorRepository repo, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CriarJogadorAsync(CriarJogadorRequest request)
    {
        if (request.UsuarioId == Guid.Empty)
            throw new ArgumentException("UsuárioId é obrigatório para criar um jogador.");

        var jogador = new Jogador(
            usuarioId: request.UsuarioId,
            nome: request.Nome,
            apelido: request.Apelido,
            telefone: request.Telefone,
            email: request.Email
        )
        {
            TamanhoCamiseta = request.TamanhoCamiseta,
            DataNascimento = request.DataNascimento
        };

        await _repo.AdicionarAsync(jogador);
        return jogador.Id;
    }

    public async Task<List<Jogador>> ObterTodosAsync()
    {
        return await _repo.ListarAsync();
    }

    public async Task<Jogador?> ObterPorIdAsync(Guid id)
    {
        return await _repo.ObterPorIdAsync(id);
    }

    public async Task AtualizarNomeAsync(Guid jogadorId, string? nome)
    {
        var jogador = await _repo.ObterPorIdAsync(jogadorId);
        if (jogador == null) throw new Exception("Jogador não encontrado");

        jogador.AtualizarNome(nome);
        await _unitOfWork.CommitAsync();
    }
}
