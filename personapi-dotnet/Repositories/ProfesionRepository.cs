using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
    public class ProfesionRepository : GenericRepository<Profesion>, IProfesionRepository
    {
        private readonly PersonaDbContext _context;

        public ProfesionRepository(PersonaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesion>> GetByNombreAsync(string nombre)
        {
            return await _context.Profesions
                .Where(p => p.Nom.Contains(nombre))
                .ToListAsync();
        }
    }
}