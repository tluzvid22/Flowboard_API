using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Images.Get
{

    public record GetImageByIdRequest(int Id) : IRequest<Result<ImageDTO>>;

}