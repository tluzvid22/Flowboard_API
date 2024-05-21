using API.DTOs;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Requests.Files.Get
{

    public record GetFileInfoByIdRequest(int FileId) : IRequest<Result<FileDTO>>;

}