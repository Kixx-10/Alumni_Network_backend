using Alumni.DTOS;
using Alumni.Models.Core;
using AutoMapper;

namespace Alumni.Profiles
{
    public class UserMapper:Profile
    {
        public UserMapper() {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
