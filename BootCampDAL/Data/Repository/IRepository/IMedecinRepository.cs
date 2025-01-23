using BootCampDAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Repository.IRepository
{
    public interface IMedecinRepository:IRepository<Medecin>
    {
        Task Update(Medecin medecin);
    }
}
