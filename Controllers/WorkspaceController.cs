using API.DTOs;
using API.Helpers;
using API.Requests.Workspace.Create;
using API.Requests.Workspace.Delete;
using API.Requests.Workspace.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WorkspaceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkspaceController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WorkspaceDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateWorkspaceAsync(CreateWorkspaceRequest workspace)
        {
            var result = await _mediator.Send(workspace);
            
            return result.IsSuccess ? Results.Created("/workspace", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("user/{UserId}/{Token}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkspaceDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetWorkspacesByUserIdAsync(int UserId, string Token)
        {
            var result = await _mediator.Send(new GetWorkspaceByUserIdRequest(UserId, Token));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("{WorkspaceId}/{UserId}/{Token}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkspaceDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetWorkspaceById(int WorkspaceId, int UserId, string Token)
        {
            var result = await _mediator.Send(new GetWorkspaceByIdRequest(WorkspaceId, UserId, Token));

            return result.IsSuccess ? (result.Value != null ? Results.Ok(result.Value) : Results.NotFound(null)) : result.Errors.ToBadRequest();
        }

        [HttpDelete("{WorkspaceId}/{UserId}/{Token}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkspaceDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> DeleteWorkspaceById(int WorkspaceId, int UserId, string Token)
        {
            var result = await _mediator.Send(new DeleteWorkspaceByIdRequest(WorkspaceId, UserId, Token));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

    }
}
