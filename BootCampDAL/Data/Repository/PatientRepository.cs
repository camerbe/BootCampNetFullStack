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
    class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly BootCampDalContext _context;

        public PatientRepository(BootCampDalContext context):base(context)
        {
            _context = context;
        }

        public async Task Update(Patient patient)
        {
            _context.Patients.Attach(patient);
            _context.Entry(patient).State = EntityState.Modified;
        }

        //Task  IPatientRepository.Update(Patient patient)
        //{
        //    _context.Patients.Update(patient);
        //    return Task.CompletedTask;
        //}
    }
}
