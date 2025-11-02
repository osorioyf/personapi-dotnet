using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Interfaces
{
    public interface IPersonaRepository : IGenericRepository<Persona>
    {
        Task<Persona> GetByCcAsync(int cc);
        Task<IEnumerable<Persona>> GetByGeneroAsync(string genero);
        Task<IEnumerable<Persona>> GetByEdadAsync(int edad);
    }
}