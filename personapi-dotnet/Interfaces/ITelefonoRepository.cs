using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface ITelefonoRepository : IGenericRepository<Telefono>
    {
        Task<IEnumerable<Telefono>> GetByPersonaAsync(int duenio);
        Task<Telefono> GetByNumeroAsync(string numero);
    }
}