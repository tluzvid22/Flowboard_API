using API.DTOs;
using Data.Entities;
using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Requests.Task.Create
{
    public record CreateTaskRequest : IRequest<Result<TaskDTO>>
    {
        public string Name { get; set; } = string.Empty;

        public int ListId { get; set; }
    }

}