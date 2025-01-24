using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IPatientRepository Patient { get; }
        IUserRepository User { get; }
        IMedecinRepository Medecin { get; }
        Task Save();
    }
}
