using API.DTOs;
using Data.Entities;
using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Requests.Workspace.Create
{
    public record CreateWorkspaceRequest : IRequest<Result<WorkspaceDTO>>
    {
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

}