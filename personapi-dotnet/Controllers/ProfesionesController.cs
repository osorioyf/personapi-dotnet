using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionesController : ControllerBase
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionesController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        // GET: api/profesiones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetAll()
        {
            try
            {
                var profesiones = await _profesionRepository.GetAllAsync();
                return Ok(profesiones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener profesiones", error = ex.Message });
            }
        }

        // GET: api/profesiones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesion>> GetById(int id)
        {
            try
            {
                var profesion = await _profesionRepository.GetByIdAsync(id);
                if (profesion == null)
                    return NotFound(new { mensaje = "Profesión no encontrada" });
                return Ok(profesion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener la profesión", error = ex.Message });
            }
        }

        // GET: api/profesiones/buscar/nombre
        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetByNombre(string nombre)
        {
            try
            {
                var profesiones = await _profesionRepository.GetByNombreAsync(nombre);
                return Ok(profesiones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al buscar profesiones", error = ex.Message });
            }
        }

        // POST: api/profesiones
        [HttpPost]
        public async Task<ActionResult<Profesion>> Create([FromBody] Profesion profesion)
        {
            try
            {
                if (profesion == null)
                    return BadRequest(new { mensaje = "Los datos de la profesión son requeridos" });

                var nuevaProfesion = await _profesionRepository.AddAsync(profesion);
                await _profesionRepository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = nuevaProfesion.Id }, nuevaProfesion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear la profesión", error = ex.Message });
            }
        }

        // PUT: api/profesiones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Profesion profesion)
        {
            try
            {
                var profesionExistente = await _profesionRepository.GetByIdAsync(id);
                if (profesionExistente == null)
                    return NotFound(new { mensaje = "Profesión no encontrada" });

                profesion.Id = id;
                await _profesionRepository.UpdateAsync(profesion);
                await _profesionRepository.SaveChangesAsync();
                return Ok(new { mensaje = "Profesión actualizada exitosamente", data = profesion });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar la profesión", error = ex.Message });
            }
        }

        // DELETE: api/profesiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _profesionRepository.DeleteAsync(id);
                if (!resultado)
                    return NotFound(new { mensaje = "Profesión no encontrada" });

                await _profesionRepository.SaveChangesAsync();
                return Ok(new { mensaje = "Profesión eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar la profesión", error = ex.Message });
            }
        }
    }
}