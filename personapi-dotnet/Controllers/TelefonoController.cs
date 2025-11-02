using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public TelefonosController(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        // GET: api/telefonos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetAll()
        {
            try
            {
                var telefonos = await _telefonoRepository.GetAllAsync();
                return Ok(telefonos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener teléfonos", error = ex.Message });
            }
        }

        // GET: api/telefonos/1234567890
        [HttpGet("{numero}")]
        public async Task<ActionResult<Telefono>> GetByNumero(string numero)
        {
            try
            {
                var telefono = await _telefonoRepository.GetByNumeroAsync(numero);
                if (telefono == null)
                    return NotFound(new { mensaje = "Teléfono no encontrado" });
                return Ok(telefono);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener el teléfono", error = ex.Message });
            }
        }

        // GET: api/telefonos/persona/123456
        [HttpGet("persona/{duenio}")]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetByPersona(int duenio)
        {
            try
            {
                var telefonos = await _telefonoRepository.GetByPersonaAsync(duenio);
                return Ok(telefonos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener teléfonos por persona", error = ex.Message });
            }
        }

        // POST: api/telefonos
        [HttpPost]
        public async Task<ActionResult<Telefono>> Create([FromBody] Telefono telefono)
        {
            try
            {
                if (telefono == null)
                    return BadRequest(new { mensaje = "Los datos del teléfono son requeridos" });

                var nuevoTelefono = await _telefonoRepository.AddAsync(telefono);
                await _telefonoRepository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByNumero), new { numero = nuevoTelefono.Num }, nuevoTelefono);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear el teléfono", error = ex.Message });
            }
        }

        // PUT: api/telefonos/1234567890
        [HttpPut("{numero}")]
        public async Task<IActionResult> Update(string numero, [FromBody] Telefono telefono)
        {
            try
            {
                var telefonoExistente = await _telefonoRepository.GetByNumeroAsync(numero);
                if (telefonoExistente == null)
                    return NotFound(new { mensaje = "Teléfono no encontrado" });

                telefono.Num = numero;
                await _telefonoRepository.UpdateAsync(telefono);
                await _telefonoRepository.SaveChangesAsync();
                return Ok(new { mensaje = "Teléfono actualizado exitosamente", data = telefono });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el teléfono", error = ex.Message });
            }
        }

        // DELETE: api/telefonos/1234567890
        [HttpDelete("{numero}")]
        public async Task<IActionResult> Delete(string numero)
        {
            try
            {
                var resultado = await _telefonoRepository.DeleteAsync(numero);
                if (!resultado)
                    return NotFound(new { mensaje = "Teléfono no encontrado" });

                await _telefonoRepository.SaveChangesAsync();
                return Ok(new { mensaje = "Teléfono eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el teléfono", error = ex.Message });
            }
        }
    }
}