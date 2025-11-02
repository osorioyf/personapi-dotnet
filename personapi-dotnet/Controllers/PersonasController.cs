using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonasController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        // GET: api/personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
        {
            try
            {
                var personas = await _personaRepository.GetAllAsync();
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener personas", error = ex.Message });
            }
        }

        // GET: api/personas/5
        [HttpGet("{cc}")]
        public async Task<ActionResult<Persona>> GetByCc(int cc)
        {
            try
            {
                var persona = await _personaRepository.GetByCcAsync(cc);
                if (persona == null)
                    return NotFound(new { mensaje = "Persona no encontrada" });
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la persona", error = ex.Message });
            }
        }

        // GET: api/personas/genero/M
        [HttpGet("genero/{genero}")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetByGenero(string genero)
        {
            try
            {
                var personas = await _personaRepository.GetByGeneroAsync(genero);
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al buscar personas por género", error = ex.Message });
            }
        }

        // GET: api/personas/edad/30
        [HttpGet("edad/{edad}")]
        public async Task<ActionResult<IEnumerable<Persona>>> GetByEdad(int edad)
        {
            try
            {
                var personas = await _personaRepository.GetByEdadAsync(edad);
                return Ok(personas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al buscar personas por edad", error = ex.Message });
            }
        }

        // POST: api/personas
        [HttpPost]
        public async Task<ActionResult<Persona>> Create([FromBody] Persona persona)
        {
            try
            {
                if (persona == null)
                    return BadRequest(new { mensaje = "Los datos de la persona son requeridos" });

                var nuevaPersona = await _personaRepository.AddAsync(persona);
                await _personaRepository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByCc), new { cc = nuevaPersona.Cc }, nuevaPersona);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la persona", error = ex.Message });
            }
        }

        // PUT: api/personas/5
        [HttpPut("{cc}")]
        public async Task<IActionResult> Update(int cc, [FromBody] Persona persona)
        {
            try
            {
                var personaExistente = await _personaRepository.GetByCcAsync(cc);
                if (personaExistente == null)
                    return NotFound(new { mensaje = "Persona no encontrada" });

                persona.Cc = cc;
                await _personaRepository.UpdateAsync(persona);
                await _personaRepository.SaveChangesAsync();
                return Ok(new { mensaje = "Persona actualizada exitosamente", data = persona });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar la persona", error = ex.Message });
            }
        }

        // DELETE: api/personas/5
        [HttpDelete("{cc}")]
        public async Task<IActionResult> Delete(int cc)
        {
            try
            {
                var resultado = await _personaRepository.DeleteAsync(cc);
                if (!resultado)
                    return NotFound(new { mensaje = "Persona no encontrada" });

                await _personaRepository.SaveChangesAsync();
                return Ok(new { mensaje = "Persona eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar la persona", error = ex.Message });
            }
        }
    }
}