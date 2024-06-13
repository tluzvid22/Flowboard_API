using API.DTOs;
using API.Helpers;
using API.Requests.Friend.Delete;
using API.Requests.Friend.Get;
using API.Requests.Task.Delete;
using API.Requests.Task.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FriendDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetFriendByIdAsync(int UserId)
        {
            var result = await _mediator.Send(new GetFriendByIdRequest(UserId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpDelete("{User1Id}/{User2Id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FriendDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> DeleteTaskAsync(int User1Id, int User2Id)
        {
            var result = await _mediator.Send(new DeleteFriendByIdRequest(User1Id, User2Id));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }
    }
}
