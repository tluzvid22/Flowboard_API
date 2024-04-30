using API.DTOs;
using API.Helpers;
using API.Requests.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateUserAsync([FromBody] CreateUserRequest user)
        {
            var result = await _mediator.Send(user);
            
            return result.IsSuccess ? Results.Created("/user", result.Value) : result.Errors.ToBadRequest();
        }
    }
}
