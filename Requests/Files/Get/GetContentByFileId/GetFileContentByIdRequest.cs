using API.DTOs;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Requests.Files.Get
{

    public record GetFileContentByIdRequest(int FileId) : IRequest<Result<FileContentResult>>;

}