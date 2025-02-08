using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.Repository;
using BootCampNetFullStack.BootCampDAL.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Repository
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly BootCampDalContext _context;

        public UnitOfWork(BootCampDalContext context)
        {
            _context = context;
            Patient = new PatientRepository(_context);
            User = new UserRepository(_context);
            Medecin = new MedecinRepository(_context);
            Specialite = new SpecialiteRepository(_context);
            RendezVous = new RendezVousRepository(_context);
        }

        public IPatientRepository Patient { get; private set; }
        public IUserRepository User { get; private set; }
        public IMedecinRepository Medecin { get; private set; }
        public ISpecialiteRepository Specialite { get; private set; }
        public IRendezVousRepository RendezVous { get; private set; }

        void IDisposable.Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        async Task IUnitOfWork.Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
