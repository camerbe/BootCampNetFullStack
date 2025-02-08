using BootCampDAL;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository;
using BootCampNetFullStack.BootCampDAL.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BootCampNetFullStack.BootCampDAL.Data.Repository
{
    
    public class RendezVousRepository : Repository<RendezVous>, IRendezVousRepository
    {
        private readonly BootCampDalContext _context;
        public RendezVousRepository(BootCampDalContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(RendezVous rendezVous)
        {
            _context.RendezVous.Attach(rendezVous);
            _context.Entry(rendezVous).State = EntityState.Modified;

        }
    }
}
