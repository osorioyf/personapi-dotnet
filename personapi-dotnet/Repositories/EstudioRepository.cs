using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repositories
{
    public class EstudioRepository : GenericRepository<Estudio>, IEstudioRepository
    {
        private readonly PersonaDbContext _context;

        public EstudioRepository(PersonaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudio>> GetByPersonaAsync(int ccPersona)
        {
            return await _context.Estudios
                .Where(e => e.CcPer == ccPersona)
                .Include(e => e.IdProfNavigation)
                .ToListAsync();
        }

        public async Task<IEnumerable<Estudio>> GetByProfesionAsync(int idProfesion)
        {
            return await _context.Estudios
                .Where(e => e.IdProf == idProfesion)
                .Include(e => e.CcPerNavigation)
                .ToListAsync();
        }
    }
}