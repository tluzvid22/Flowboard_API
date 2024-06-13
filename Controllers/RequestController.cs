using API.DTOs;
using API.Helpers;
using API.Requests.Request.Create;
using API.Requests.Request.Delete;
using API.Requests.Request.Get;
using API.Requests.Request.Update;
using API.Requests.Task.Create;
using API.Requests.Task.Delete;
using API.Requests.Task.Update;
using Data.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequestController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RequestDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetRequestByIdAsync(int UserId)
        {
            var result = await _mediator.Send(new GetRequestByIdRequest(UserId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RequestDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateTaskAsync(CreateRequestRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? Results.Created("/task", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RequestDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> UpdateTaskAsync(UpdateRequestRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpDelete("{User1Id}/{User2Id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RequestDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> DeleteTaskAsync(int User1Id, int User2Id)
        {
            var result = await _mediator.Send(new DeleteRequestByIdRequest(User1Id, User2Id));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }
    }
}
