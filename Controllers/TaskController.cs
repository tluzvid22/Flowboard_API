using API.DTOs;
using API.Helpers;
using API.Requests.Task.Create;
using API.Requests.Task.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TaskDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateTaskAsync(CreateTaskRequest task)
        {
            var result = await _mediator.Send(task);
            
            return result.IsSuccess ? Results.Created("/task", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("list/{ListId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetTasksByListIdAsync(int ListId)
        {
            var result = await _mediator.Send(new GetTaskByListIdRequest(ListId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("{TaskId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaskDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetTaskById(int TaskId)
        {
            var result = await _mediator.Send(new GetTaskByIdRequest(TaskId));

            return result.IsSuccess ? (result.Value != null ? Results.Ok(result.Value) : Results.NotFound(null)) : result.Errors.ToBadRequest();
        }

    }
}
