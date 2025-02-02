using AutoMapper;
using BootCampDAL.Data.DTO;
using BootCampDAL.Data.Models;
using BootCampNetFullStack.BootCampDAL.Data.DTO;

namespace BootCampNetFullStack.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<User, UserResponseDTO>().ReverseMap();
        }
    }
}
