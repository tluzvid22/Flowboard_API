using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Task.Update
{
    public record UpdateTaskRequest: IRequest<Result<TaskDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }

        public int ListId { get; set; }
    }
}