using API.DTOs;
using API.Requests.Collaborators.Create;
using API.Requests.Collaborators.Delete;
using API.Requests.Collaborators.Update;
using API.Requests.Files.Create;
using API.Requests.List.Create;
using API.Requests.List.Update;
using API.Requests.Request.Create;
using API.Requests.Task.Create;
using API.Requests.Task.Update;
using API.Requests.Token.Create;
using API.Requests.Users.Create;
using API.Requests.UserTasks.Create;
using API.Requests.Workspace.Create;
using AutoMapper;
using Data.Entities;

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
            
            CreateMap<User, PublicUserDTO>();

            CreateMap<UserTask, UserTaskDTO>();
            CreateMap<UserTaskDTO, UserTask>();
            CreateMap<CreateUserTaskRequest, UserTask>();

            CreateMap<Collaborator, CollaboratorDTO>();
            CreateMap<CollaboratorDTO, Collaborator>();
            CreateMap<CreateCollaboratorRequest, Collaborator>();
            CreateMap<UpdateCollaboratorRequest, Collaborator>();

            CreateMap<Friend, FriendDTO>().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User1?? src.User2))
                                          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User1 != null ? src.User1Id : src.User2Id));

            CreateMap<Request, RequestDTO>().ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Sender ?? src.Receiver))
                                          .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Sender != null ? src.SenderId : src.ReceiverId));

            CreateMap<CreateRequestRequest, Request>().ForMember(dest => dest.RequestedByUserId, opt => opt.MapFrom(src => src.SenderId));
            
            CreateMap<CreateTokenRequest, Token>();
            CreateMap<Token, TokenDTO>();

            CreateMap<CreateFileRequest, Data.Entities.Files>().ForMember(dest => dest.File, opt => opt.MapFrom(src => MapFile(src.File)));
            CreateMap<UpdateTaskIdOnFileRequest, Data.Entities.Files>();
            CreateMap<Data.Entities.Files, FileDTO>().ForMember(dest => dest.ContentUrl, opt => opt.MapFrom(src => FileContentUrl+src.Id));

            CreateMap<CreateTaskRequest, Data.Entities.Task>();
            CreateMap<UpdateTaskRequest, Data.Entities.Task>();
            CreateMap<Data.Entities.Task, TaskDTO>();
            
            CreateMap<CreateListRequest, Data.Entities.List>();
            CreateMap<UpdateListRequest, Data.Entities.List>();
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
        
        private static FriendDTO MapFriend(Friend friend)
        {
            return null!;
        }

        private static Request MapRequest()
        {
            return null;
        }
    }
}
