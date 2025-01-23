using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
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
        }

        public IPatientRepository Patient {  get; private set; }

        public IUserRepository User { get; private set; }
        public IMedecinRepository Medecin { get; private set; }

        Task IUnitOfWork.Save()
        {
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
