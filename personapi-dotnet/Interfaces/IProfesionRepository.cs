using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface IProfesionRepository : IGenericRepository<Profesion>
    {
        Task<IEnumerable<Profesion>> GetByNombreAsync(string nombre);
    }
}