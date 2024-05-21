using API.DTOs;
using API.Helpers;
using API.Requests.Token.Create;
using API.Requests.Token.Delete;
using API.Requests.Token.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TokenDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateTokenAsync(CreateTokenRequest token)
        {
            var result = await _mediator.Send(token);

            return result.IsSuccess ? Results.Created("/token", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TokenDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetTokensAsync(int UserId)
        {
            var result = await _mediator.Send(new GetTokenByUserIdRequest(UserId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpDelete("user/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TokenDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> DeleteTokenByUserIdAsync(int UserId)
        {
            var result = await _mediator.Send(new DeleteTokenByUserIdRequest(UserId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }
        
        [HttpDelete("token/{Token}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TokenDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> DeleteTokenByTokenValueAsync(string Token)
        {
            var result = await _mediator.Send(new DeleteTokenByTokenValueRequest(Token));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }
    }
}
