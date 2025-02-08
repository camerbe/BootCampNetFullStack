using BootCampDAL;
using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BootCampNetFullStack.BootCampDAL.Data.Repository
{
    public class SpecialiteRepository : Repository<Specialite>, ISpecialiteRepository
    {
        private readonly BootCampDalContext _context;

        public SpecialiteRepository(BootCampDalContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Specialite specialite)
        {
            _context.Specialites.Attach(specialite);
            _context.Entry(specialite).State = EntityState.Modified;
            
        }
    }
}
