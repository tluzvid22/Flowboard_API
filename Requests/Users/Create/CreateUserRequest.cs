using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Users.Create
{
    public record CreateUserRequest : IRequest<Result<UserDTO>>
    {
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Adress { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }

}