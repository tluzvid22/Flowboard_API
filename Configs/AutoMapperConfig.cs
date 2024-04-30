using API.DTOs;
using API.Requests.Users.Create;
using AutoMapper;
using Data.Entities;

namespace API.Configs
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {
            UserMappings();
        }

        private void UserMappings()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
