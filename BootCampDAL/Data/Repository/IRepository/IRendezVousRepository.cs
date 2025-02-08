using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;

namespace BootCampNetFullStack.BootCampDAL.Data.Repository.IRepository
{
    public interface IRendezVousRepository : IRepository<RendezVous>
    {
        Task Update(RendezVous rendezVous);
    }
}
