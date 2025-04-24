using QNF.Plataforma.Application.Grupos.Commands;
using QNF.Plataforma.Core.Entities;
using QNF.Plataforma.Core.Interfaces;

namespace QNF.Plataforma.Application.Grupos.Handlers;

public class AdicionarJogadorAoGrupoHandler
{
    private readonly IGrupoRepository _grupoRepository;
    private readonly IJogadorRepository _jogadorRepository;
    private readonly IJogadorGrupoRepository _jogadorGrupoRepository;

    public AdicionarJogadorAoGrupoHandler(
        IGrupoRepository grupoRepository,
        IJogadorRepository jogadorRepository,
        IJogadorGrupoRepository jogadorGrupoRepository)
    {
        _grupoRepository = grupoRepository;
        _jogadorRepository = jogadorRepository;
        _jogadorGrupoRepository = jogadorGrupoRepository;
    }

    public async Task Handle(AdicionarJogadorAoGrupoCommand command)
    {
        // 1. Validar se o grupo existe
        var grupo = await _grupoRepository.ObterPorIdAsync(command.GrupoId);
        if (grupo is null)
            throw new Exception("Grupo não encontrado.");

        // 2. Validar se o jogador existe
        var jogador = await _jogadorRepository.ObterPorIdAsync(command.JogadorId);
        if (jogador is null)
            throw new Exception("Jogador não encontrado.");

        // 3. Verificar se o jogador já está no grupo
        var jaEstaNoGrupo = await _jogadorGrupoRepository.ExisteAsync(command.JogadorId, command.GrupoId);
        if (jaEstaNoGrupo)
            throw new Exception("Jogador já está no grupo.");

        // 4. Criar vínculo
        var jogadorGrupo = new JogadorGrupo(command.JogadorId, command.GrupoId);
        await _jogadorGrupoRepository.AdicionarAsync(jogadorGrupo);
    }
}