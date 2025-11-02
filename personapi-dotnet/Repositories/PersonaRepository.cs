using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
    public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
    {
        private readonly PersonaDbContext _context;

        public PersonaRepository(PersonaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Persona> GetByCcAsync(int cc)
        {
            return await _context.Personas
                .FirstOrDefaultAsync(p => p.Cc == cc);
        }

        public async Task<IEnumerable<Persona>> GetByGeneroAsync(string genero)
        {
            return await _context.Personas
                .Where(p => p.Genero == genero)
                .ToListAsync();
        }

        public async Task<IEnumerable<Persona>> GetByEdadAsync(int edad)
        {
            return await _context.Personas
                .Where(p => p.Edad == edad)
                .ToListAsync();
        }
    }
}