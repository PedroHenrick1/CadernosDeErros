using CadernosDeErros.Entities;
using CadernosDeErros.DTOs;
using CadernosDeErros.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadernosDeErros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MateriaController> _logger;

        public MateriaController(ApplicationDbContext context, ILogger<MateriaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Materia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaDto>>> GetMaterias()
        {
            var materias = await _context.Materias
                .Include(m => m.Assuntos)
                .ToListAsync();

            return materias.Select(m => new MateriaDto
            {
                Id = m.Id,
                Nome = m.Nome,
                DataCriacao = m.DataCriacao,
                QuantidadeAssuntos = m.Assuntos.Count
            }).ToList();
        }

        // GET: api/Materia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDto>> GetMateria(int id)
        {
            var materia = await _context.Materias
                .Include(m => m.Assuntos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (materia == null)
            {
                return NotFound(new { message = "Matéria não encontrada" });
            }

            var materiaDto = new MateriaDto
            {
                Id = materia.Id,
                Nome = materia.Nome,
                DataCriacao = materia.DataCriacao,
                QuantidadeAssuntos = materia.Assuntos.Count
            };

            return materiaDto;
        }

        // POST: api/Materia
        [HttpPost]
        public async Task<ActionResult<MateriaDto>> PostMateria(CreateMateriaDto createDto)
        {
            var materia = new Materia
            {
                Nome = createDto.Nome,
                DataCriacao = DateTime.UtcNow
            };

            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            var materiaDto = new MateriaDto
            {
                Id = materia.Id,
                Nome = materia.Nome,
                DataCriacao = materia.DataCriacao,
                QuantidadeAssuntos = 0
            };

            return CreatedAtAction(nameof(GetMateria), new { id = materia.Id }, materiaDto);
        }

        // PUT: api/Materia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(int id, UpdateMateriaDto updateDto)
        {
            var materia = await _context.Materias.FindAsync(id);
            
            if (materia == null)
            {
                return NotFound(new { message = "Matéria não encontrada" });
            }

            // Atualiza apenas os campos que foram enviados
            if (updateDto.Nome != null)
            {
                materia.Nome = updateDto.Nome;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Materia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound(new { message = "Matéria não encontrada" });
            }

            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriaExists(int id)
        {
            return _context.Materias.Any(e => e.Id == id);
        }
    }
}