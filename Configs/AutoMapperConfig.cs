using API.DTOs;
using API.Requests.Images.Create;
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
            CreateMap<CreateImageRequest, Image>().ForMember(dest => dest.File, opt => opt.MapFrom(src => MapImage(src.File)));
            CreateMap<Image, ImageDTO>();
        }

        private static byte[] MapImage(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
