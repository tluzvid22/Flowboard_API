using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Friend.Get
{

    public record GetFriendByIdRequest(int UserId) : IRequest<Result<FriendDTO[]>>;

}