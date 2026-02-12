using CadernosDeErros.Entities;
using CadernosDeErros.DTOs;
using CadernosDeErros.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadernosDeErros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ErroController> _logger;

        public ErroController(ApplicationDbContext context, ILogger<ErroController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Erro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErroDto>>> GetErros()
        {
            var erros = await _context.Erros
                .Include(e => e.Assunto)
                    .ThenInclude(a => a.Materia)
                .OrderByDescending(e => e.DataErro)
                .ToListAsync();

            return erros.Select(e => new ErroDto
            {
                Id = e.Id,
                Questao = e.Questao,
                RespostaCorreta = e.RespostaCorreta,
                MinhaResposta = e.MinhaResposta,
                Explicacao = e.Explicacao,
                Observacoes = e.Observacoes,
                AssuntoId = e.AssuntoId,
                NomeAssunto = e.Assunto.Nome,
                NomeMateria = e.Assunto.Materia.Nome,
                DataErro = e.DataErro,
                DataRevisao = e.DataRevisao,
                Revisado = e.Revisado
            }).ToList();
        }

        // GET: api/Erro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErroDto>> GetErro(int id)
        {
            var erro = await _context.Erros
                .Include(e => e.Assunto)
                    .ThenInclude(a => a.Materia)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (erro == null)
            {
                return NotFound(new { message = "Erro n�o encontrado" });
            }

            var erroDto = new ErroDto
            {
                Id = erro.Id,
                Questao = erro.Questao,
                RespostaCorreta = erro.RespostaCorreta,
                MinhaResposta = erro.MinhaResposta,
                Explicacao = erro.Explicacao,
                Observacoes = erro.Observacoes,
                AssuntoId = erro.AssuntoId,
                NomeAssunto = erro.Assunto.Nome,
                NomeMateria = erro.Assunto.Materia.Nome,
                DataErro = erro.DataErro,
                DataRevisao = erro.DataRevisao,
                Revisado = erro.Revisado
            };

            return erroDto;
        }

        // GET: api/Erro/Assunto/5
        [HttpGet("Assunto/{assuntoId}")]
        public async Task<ActionResult<IEnumerable<ErroDto>>> GetErrosByAssunto(int assuntoId)
        {
            var erros = await _context.Erros
                .Where(e => e.AssuntoId == assuntoId)
                .Include(e => e.Assunto)
                    .ThenInclude(a => a.Materia)
                .OrderByDescending(e => e.DataErro)
                .ToListAsync();

            return erros.Select(e => new ErroDto
            {
                Id = e.Id,
                Questao = e.Questao,
                RespostaCorreta = e.RespostaCorreta,
                MinhaResposta = e.MinhaResposta,
                Explicacao = e.Explicacao,
                Observacoes = e.Observacoes,
                AssuntoId = e.AssuntoId,
                NomeAssunto = e.Assunto.Nome,
                NomeMateria = e.Assunto.Materia.Nome,
                DataErro = e.DataErro,
                DataRevisao = e.DataRevisao,
                Revisado = e.Revisado
            }).ToList();
        }

        // GET: api/Erro/Materia/5
        [HttpGet("Materia/{materiaId}")]
        public async Task<ActionResult<IEnumerable<ErroDto>>> GetErrosByMateria(int materiaId)
        {
            var erros = await _context.Erros
                .Include(e => e.Assunto)
                    .ThenInclude(a => a.Materia)
                .Where(e => e.Assunto.MateriaId == materiaId)
                .OrderByDescending(e => e.DataErro)
                .ToListAsync();

            return erros.Select(e => new ErroDto
            {
                Id = e.Id,
                Questao = e.Questao,
                RespostaCorreta = e.RespostaCorreta,
                MinhaResposta = e.MinhaResposta,
                Explicacao = e.Explicacao,
                Observacoes = e.Observacoes,
                AssuntoId = e.AssuntoId,
                NomeAssunto = e.Assunto.Nome,
                NomeMateria = e.Assunto.Materia.Nome,
                DataErro = e.DataErro,
                DataRevisao = e.DataRevisao,
                Revisado = e.Revisado
            }).ToList();
        }

        // GET: api/Erro/NaoRevisados
        [HttpGet("NaoRevisados")]
        public async Task<ActionResult<IEnumerable<ErroDto>>> GetErrosNaoRevisados()
        {
            var erros = await _context.Erros
                .Include(e => e.Assunto)
                    .ThenInclude(a => a.Materia)
                .Where(e => !e.Revisado)
                .OrderByDescending(e => e.DataErro)
                .ToListAsync();

            return erros.Select(e => new ErroDto
            {
                Id = e.Id,
                Questao = e.Questao,
                RespostaCorreta = e.RespostaCorreta,
                MinhaResposta = e.MinhaResposta,
                Explicacao = e.Explicacao,
                Observacoes = e.Observacoes,
                AssuntoId = e.AssuntoId,
                NomeAssunto = e.Assunto.Nome,
                NomeMateria = e.Assunto.Materia.Nome,
                DataErro = e.DataErro,
                DataRevisao = e.DataRevisao,
                Revisado = e.Revisado
            }).ToList();
        }

        // POST: api/Erro
        [HttpPost]
        public async Task<ActionResult<ErroDto>> PostErro(CreateErroDto createDto)
        {
            // Verifica se o assunto existe
            var assunto = await _context.Assuntos
                .Include(a => a.Materia)
                .FirstOrDefaultAsync(a => a.Id == createDto.AssuntoId);
            
            if (assunto == null)
            {
                return BadRequest(new { message = "Assunto n�o encontrado" });
            }

            var erro = new Erro
            {
                Questao = createDto.Questao,
                RespostaCorreta = createDto.RespostaCorreta,
                MinhaResposta = createDto.MinhaResposta,
                Explicacao = createDto.Explicacao,
                Observacoes = createDto.Observacoes,
                AssuntoId = createDto.AssuntoId,
                DataErro = DateTime.UtcNow
            };

            _context.Erros.Add(erro);
            await _context.SaveChangesAsync();

            var erroDto = new ErroDto
            {
                Id = erro.Id,
                Questao = erro.Questao,
                RespostaCorreta = erro.RespostaCorreta,
                MinhaResposta = erro.MinhaResposta,
                Explicacao = erro.Explicacao,
                Observacoes = erro.Observacoes,
                AssuntoId = erro.AssuntoId,
                NomeAssunto = assunto.Nome,
                NomeMateria = assunto.Materia.Nome,
                DataErro = erro.DataErro,
                DataRevisao = erro.DataRevisao,
                Revisado = erro.Revisado
            };

            return CreatedAtAction(nameof(GetErro), new { id = erro.Id }, erroDto);
        }

        // PUT: api/Erro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutErro(int id, UpdateErroDto updateDto)
        {
            var erro = await _context.Erros.FindAsync(id);
            
            if (erro == null)
            {
                return NotFound(new { message = "Erro n�o encontrado" });
            }

            // Atualiza apenas os campos que foram enviados
            if (updateDto.Questao != null)
            {
                erro.Questao = updateDto.Questao;
            }

            if (updateDto.RespostaCorreta != null)
            {
                erro.RespostaCorreta = updateDto.RespostaCorreta;
            }

            if (updateDto.MinhaResposta != null)
            {
                erro.MinhaResposta = updateDto.MinhaResposta;
            }

            if (updateDto.Explicacao != null)
            {
                erro.Explicacao = updateDto.Explicacao;
            }

            if (updateDto.Observacoes != null)
            {
                erro.Observacoes = updateDto.Observacoes;
            }

            if (updateDto.AssuntoId.HasValue)
            {
                // Verifica se o novo assunto existe
                var assuntoExists = await _context.Assuntos.AnyAsync(a => a.Id == updateDto.AssuntoId.Value);
                if (!assuntoExists)
                {
                    return BadRequest(new { message = "Assunto n�o encontrado" });
                }
                erro.AssuntoId = updateDto.AssuntoId.Value;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Erro/5/Revisar
        [HttpPatch("{id}/Revisar")]
        public async Task<IActionResult> MarcarComoRevisado(int id)
        {
            var erro = await _context.Erros.FindAsync(id);
            if (erro == null)
            {
                return NotFound(new { message = "Erro n�o encontrado" });
            }

            erro.Revisado = true;
            erro.DataRevisao = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Erro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteErro(int id)
        {
            var erro = await _context.Erros.FindAsync(id);
            if (erro == null)
            {
                return NotFound(new { message = "Erro n�o encontrado" });
            }

            _context.Erros.Remove(erro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ErroExists(int id)
        {
            return _context.Erros.Any(e => e.Id == id);
        }
    }
}