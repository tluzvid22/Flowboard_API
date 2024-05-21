using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.File.Get
{

    public record GetFileByIdRequest(int FileId) : IRequest<Result<FileDTO>>;

}