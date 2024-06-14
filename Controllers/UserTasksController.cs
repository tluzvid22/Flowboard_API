using API.DTOs;
using API.Helpers;
using API.Requests.Collaborators.Create;
using API.Requests.Collaborators.Delete;
using API.Requests.Collaborators.Get;
using API.Requests.Collaborators.Update;
using API.Requests.Users.Get;
using API.Requests.UserTasks.Create;
using API.Requests.UserTasks.Delete;
using API.Requests.UserTasks.Get;
using Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserTasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserTasksController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{TaskId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserTaskDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetUsersAsync(int TaskId)
        {
            var result = await _mediator.Send(new GetUserTasksRequest(TaskId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserTaskDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateCollaboratorAsync(CreateUserTaskRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? Results.Created("/usertask", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpDelete("{UserId}/{TaskId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> DeleteCollaboratorAsync(int UserId, int TaskId)
        {
            var result = await _mediator.Send(new DeleteUserTaskRequest(UserId, TaskId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }
    }
}
