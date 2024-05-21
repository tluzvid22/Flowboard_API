using API.DTOs;
using API.Helpers;
using API.Requests.Users.Create;
using API.Requests.Users.Get;
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
        public async Task<IResult> CreateUserAsync(CreateUserRequest user)
        {
            var result = await _mediator.Send(user);
            
            return result.IsSuccess ? Results.Created("/user", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetUserByIdAsync(int UserId)
        {
            var result = await _mediator.Send(new GetUserByIdRequest(UserId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        } 
        
        [HttpGet("token/{Token}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetUserByTokenAsync(string Token)
        {
            var result = await _mediator.Send(new GetUserByTokenRequest(Token));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("{email}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetUserById(string email, string password)
        {
            var result = await _mediator.Send(new GetUserByEmailAndPasswordRequest(email, password));

            return result.IsSuccess ? (result.Value != null ? Results.Ok(result.Value) : Results.NotFound(null)) : result.Errors.ToBadRequest();
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetEmailExists(string email)
        {
            var result = await _mediator.Send(new GetEmailExistsRequest(email));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("username/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetUsernameExists(string username)
        {
            var result = await _mediator.Send(new GetUsernameExistsRequest(username));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

    }
}
