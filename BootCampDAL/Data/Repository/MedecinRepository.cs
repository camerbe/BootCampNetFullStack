using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Repository
{
    class MedecinRepository : Repository<Medecin>, IMedecinRepository
    {
        private readonly BootCampDalContext _context;
        public MedecinRepository(BootCampDalContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Medecin medecin)
        {
            _context.Medecins.Attach(medecin);
            _context.Entry(medecin).State = EntityState.Modified;
        }

        //Task IMedecinRepository.Update(Medecin medecin)
        //{
        //    _context.Medecins.Update(medecin);
        //    return Task.CompletedTask;
        //}
    }
}
