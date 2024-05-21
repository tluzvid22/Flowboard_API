using API.DTOs;
using Data.Entities;
using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Requests.List.Create
{
    public record CreateListRequest : IRequest<Result<ListDTO>>
    {
        public string Name { get; set; } = string.Empty;
        public int WorkspaceId { get; set; }
    }

}