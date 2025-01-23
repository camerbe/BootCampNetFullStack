using BootCampDAL.Data.Models;
using BootCampDAL.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCampDAL.Data.Repository
{
    internal class UserRepository : Repository<User>, IUserRepository
    {
        private BootCampDalContext _context;
        public UserRepository(BootCampDalContext context) : base(context)
        {
            _context = context;
        }

        Task  IUserRepository.Update(User user)
        {
           _context.Users.Update(user);
            return Task.CompletedTask;

        }
    }
}
