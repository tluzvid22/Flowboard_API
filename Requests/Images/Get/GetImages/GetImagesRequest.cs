using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Images.Get
{
    public record GetImagesRequest : IRequest<Result<ImageDTO[]>>;

}