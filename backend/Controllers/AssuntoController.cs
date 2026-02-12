using CadernosDeErros.Entities;
using CadernosDeErros.DTOs;
using CadernosDeErros.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadernosDeErros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssuntoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AssuntoController> _logger;

        public AssuntoController(ApplicationDbContext context, ILogger<AssuntoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Assunto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssuntoDto>>> GetAssuntos()
        {
            var assuntos = await _context.Assuntos
                .Include(a => a.Materia)
                .Include(a => a.Erros)
                .ToListAsync();

            return assuntos.Select(a => new AssuntoDto
            {
                Id = a.Id,
                Nome = a.Nome,
                MateriaId = a.MateriaId,
                NomeMateria = a.Materia.Nome,
                DataCriacao = a.DataCriacao,
                QuantidadeErros = a.Erros.Count
            }).ToList();
        }

        // GET: api/Assunto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssuntoDto>> GetAssunto(int id)
        {
            var assunto = await _context.Assuntos
                .Include(a => a.Materia)
                .Include(a => a.Erros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assunto == null)
            {
                return NotFound(new { message = "Assunto não encontrado" });
            }

            var assuntoDto = new AssuntoDto
            {
                Id = assunto.Id,
                Nome = assunto.Nome,
                MateriaId = assunto.MateriaId,
                NomeMateria = assunto.Materia.Nome,
                DataCriacao = assunto.DataCriacao,
                QuantidadeErros = assunto.Erros.Count
            };

            return assuntoDto;
        }

        // GET: api/Assunto/Materia/5
        [HttpGet("Materia/{materiaId}")]
        public async Task<ActionResult<IEnumerable<AssuntoDto>>> GetAssuntosByMateria(int materiaId)
        {
            var assuntos = await _context.Assuntos
                .Where(a => a.MateriaId == materiaId)
                .Include(a => a.Materia)
                .Include(a => a.Erros)
                .ToListAsync();

            return assuntos.Select(a => new AssuntoDto
            {
                Id = a.Id,
                Nome = a.Nome,
                MateriaId = a.MateriaId,
                NomeMateria = a.Materia.Nome,
                DataCriacao = a.DataCriacao,
                QuantidadeErros = a.Erros.Count
            }).ToList();
        }

        // POST: api/Assunto
        [HttpPost]
        public async Task<ActionResult<AssuntoDto>> PostAssunto(CreateAssuntoDto createDto)
        {
            // Verifica se a matéria existe
            var materia = await _context.Materias.FindAsync(createDto.MateriaId);
            if (materia == null)
            {
                return BadRequest(new { message = "Matéria não encontrada" });
            }

            var assunto = new Assunto
            {
                Nome = createDto.Nome,
                MateriaId = createDto.MateriaId,
                DataCriacao = DateTime.UtcNow
            };

            _context.Assuntos.Add(assunto);
            await _context.SaveChangesAsync();

            var assuntoDto = new AssuntoDto
            {
                Id = assunto.Id,
                Nome = assunto.Nome,
                MateriaId = assunto.MateriaId,
                NomeMateria = materia.Nome,
                DataCriacao = assunto.DataCriacao,
                QuantidadeErros = 0
            };

            return CreatedAtAction(nameof(GetAssunto), new { id = assunto.Id }, assuntoDto);
        }

        // PUT: api/Assunto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssunto(int id, UpdateAssuntoDto updateDto)
        {
            var assunto = await _context.Assuntos.FindAsync(id);
            
            if (assunto == null)
            {
                return NotFound(new { message = "Assunto não encontrado" });
            }

            // Atualiza apenas os campos que foram enviados
            if (updateDto.Nome != null)
            {
                assunto.Nome = updateDto.Nome;
            }

            if (updateDto.MateriaId.HasValue)
            {
                // Verifica se a nova matéria existe
                var materiaExists = await _context.Materias.AnyAsync(m => m.Id == updateDto.MateriaId.Value);
                if (!materiaExists)
                {
                    return BadRequest(new { message = "Matéria não encontrada" });
                }
                assunto.MateriaId = updateDto.MateriaId.Value;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Assunto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssunto(int id)
        {
            var assunto = await _context.Assuntos
                .Include(a => a.Erros)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assunto == null)
            {
                return NotFound(new { message = "Assunto não encontrado" });
            }

            // Verifica se há erros associados (devido ao Restrict no banco)
            if (assunto.Erros.Any())
            {
                return BadRequest(new { message = "Não é possível deletar assunto com erros cadastrados" });
            }

            _context.Assuntos.Remove(assunto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssuntoExists(int id)
        {
            return _context.Assuntos.Any(e => e.Id == id);
        }
    }
}