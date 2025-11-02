using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface IEstudioRepository : IGenericRepository<Estudio>
    {
        Task<IEnumerable<Estudio>> GetByPersonaAsync(int ccPersona);
        Task<IEnumerable<Estudio>> GetByProfesionAsync(int idProfesion);
    }
}