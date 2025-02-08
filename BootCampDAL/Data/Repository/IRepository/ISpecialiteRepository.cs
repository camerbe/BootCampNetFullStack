using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using BootCampNetFullStack.BootCampDAL.Data.Models;
using System.Linq.Expressions;

namespace BootCampNetFullStack.BootCampDAL.Data.Repository.IRepository
{
    public interface ISpecialiteRepository : IRepository<Specialite>
    {
        Task Update(Specialite specialite);
    }
}
