using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.List.Update
{
    public record UpdateListRequest: IRequest<Result<ListDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int WorkspaceId { get; set; }
        public int Order { get; set; }
    }
}