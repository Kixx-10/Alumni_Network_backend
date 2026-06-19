using Alumni.DTOS;
using Alumni.Models.Core;

namespace Alumni.Profiles
{
    public class UserMapper : AutoMapper.Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}