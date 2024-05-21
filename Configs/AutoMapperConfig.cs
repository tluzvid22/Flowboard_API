using API.DTOs;
using API.Requests.ExampleDelete.Create;
using API.Requests.Files.Create;
using API.Requests.List.Create;
using API.Requests.Task.Create;
using API.Requests.Token.Create;
using API.Requests.Users.Create;
using API.Requests.Workspace.Create;
using AutoMapper;
using Data.Entities;
using System.Configuration;

namespace API.Configs
{
    public class AutoMapperConfig : Profile
    {
        private readonly IConfiguration _configuration;
        private string FileContentUrl;

        public AutoMapperConfig() {
            UserMappings();
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            FileContentUrl = _configuration["Api_url:Base_Url"] + _configuration["Api_url:Endpoints:File:Content"] + "/";
        }

        private void UserMappings()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            
            CreateMap<CreateTokenRequest, Token>();
            CreateMap<Token, TokenDTO>();

            CreateMap<CreateFileRequest, Data.Entities.Files>().ForMember(dest => dest.File, opt => opt.MapFrom(src => MapFile(src.File)));
            CreateMap<UpdateTaskIdOnFileRequest, Data.Entities.Files>();
            CreateMap<Data.Entities.Files, FileDTO>().ForMember(dest => dest.ContentUrl, opt => opt.MapFrom(src => FileContentUrl+src.Id));

            CreateMap<CreateTaskRequest, Data.Entities.Task>();
            CreateMap<Data.Entities.Task, TaskDTO>();
            
            CreateMap<CreateListRequest, Data.Entities.List>();
            CreateMap<Data.Entities.List, ListDTO>();   
            
            CreateMap<CreateWorkspaceRequest, Data.Entities.Workspace>();
            CreateMap<Data.Entities.Workspace, WorkspaceDTO>();

        }

        private static byte[] MapFile(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
