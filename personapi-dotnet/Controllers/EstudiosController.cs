using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiosController : ControllerBase
    {
        private readonly IEstudioRepository _estudioRepository;

        public EstudiosController(IEstudioRepository estudioRepository)
        {
            _estudioRepository = estudioRepository;
        }

        // GET: api/estudios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudio>>> GetAll()
        {
            try
            {
                var estudios = await _estudioRepository.GetAllAsync();
                return Ok(estudios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener estudios", error = ex.Message });
            }
        }

        // GET: api/estudios/persona/123456
        [HttpGet("persona/{ccPersona}")]
        public async Task<ActionResult<IEnumerable<Estudio>>> GetByPersona(int ccPersona)
        {
            try
            {
                var estudios = await _estudioRepository.GetByPersonaAsync(ccPersona);
                return Ok(estudios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener estudios por persona", error = ex.Message });
            }
        }

        // GET: api/estudios/profesion/5
        [HttpGet("profesion/{idProfesion}")]
        public async Task<ActionResult<IEnumerable<Estudio>>> GetByProfesion(int idProfesion)
        {
            try
            {
                var estudios = await _estudioRepository.GetByProfesionAsync(idProfesion);
                return Ok(estudios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener estudios por profesión", error = ex.Message });
            }
        }

        // POST: api/estudios
        [HttpPost]
        public async Task<ActionResult<Estudio>> Create([FromBody] Estudio estudio)
        {
            try
            {
                if (estudio == null)
                    return BadRequest(new { mensaje = "Los datos del estudio son requeridos" });

                var nuevoEstudio = await _estudioRepository.AddAsync(estudio);
                await _estudioRepository.SaveChangesAsync();
                return CreatedAtAction(nameof(GetByPersona), new { ccPersona = nuevoEstudio.CcPer }, nuevoEstudio);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear el estudio", error = ex.Message });
            }
        }

        // PUT: api/estudios
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Estudio estudio)
        {
            try
            {
                if (estudio == null)
                    return BadRequest(new { mensaje = "Los datos del estudio son requeridos" });

                var estudioExistente = await _estudioRepository.GetByIdAsync(new { estudio.IdProf, estudio.CcPer });
                if (estudioExistente == null)
                    return NotFound(new { mensaje = "Estudio no encontrado" });

                await _estudioRepository.UpdateAsync(estudio);
                await _estudioRepository.SaveChangesAsync();
                return Ok(new { mensaje = "Estudio actualizado exitosamente", data = estudio });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el estudio", error = ex.Message });
            }
        }

        // DELETE: api/estudios/5/123456
        [HttpDelete("{idProf}/{ccPer}")]
        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            try
            {
                var estudio = await _estudioRepository.GetByIdAsync(new { idProf, ccPer });
                if (estudio == null)
                    return NotFound(new { mensaje = "Estudio no encontrado" });

                await _estudioRepository.DeleteAsync(new { idProf, ccPer });
                await _estudioRepository.SaveChangesAsync();
                return Ok(new { mensaje = "Estudio eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el estudio", error = ex.Message });
            }
        }
    }
}