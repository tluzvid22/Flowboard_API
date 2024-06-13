using API.DTOs;
using FluentResults;
using MediatR;

namespace API.Requests.Friend.Delete
{

    public record DeleteFriendByIdRequest(int User1Id, int User2Id) : IRequest<Result<bool>>;

}