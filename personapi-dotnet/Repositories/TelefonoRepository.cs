using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
    public class TelefonoRepository : GenericRepository<Telefono>, ITelefonoRepository
    {
        private readonly PersonaDbContext _context;

        public TelefonoRepository(PersonaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Telefono>> GetByPersonaAsync(int duenio)
        {
            return await _context.Telefonos
                .Where(t => t.Duenio == duenio)
                .ToListAsync();
        }

        public async Task<Telefono> GetByNumeroAsync(string numero)
        {
            return await _context.Telefonos
                .FirstOrDefaultAsync(t => t.Num == numero);
        }
    }
}