using API.DTOs;
using API.Helpers;
using API.Requests.Collaborators.Create;
using API.Requests.Collaborators.Delete;
using API.Requests.Collaborators.Get;
using API.Requests.Collaborators.Update;
using Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CollaboratorController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CollaboratorDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetCollaboratorByUserIdAsync(int WorkspaceId, int UserId, [FromHeader] string UserToken)
        {
            var result = await _mediator.Send(new GetCollaboratorsByUserIdRequest(UserId, UserToken));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }        
        
        [HttpGet("{UserId}/{WorkspaceId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CollaboratorDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetCollaboratorByWorkspaceIdAsync(int WorkspaceId, int UserId, [FromHeader] string UserToken)
        {
            var result = await _mediator.Send(new GetCollaboratorsByWorkspaceIdRequest(WorkspaceId, UserId, UserToken));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CollaboratorDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateCollaboratorAsync([FromHeader] int AdminId, [FromHeader] string AdminToken, CreateCollaboratorRequestBody body)
        {
            var result = await _mediator.Send(new CreateCollaboratorRequest(AdminId, AdminToken, body.UserId, body.WorkspaceId, body.CanRead, body.CanDelete, body.CanModify));

            return result.IsSuccess ? Results.Created("/collaborator", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CollaboratorDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> UpdateCollaboratorAsync([FromHeader] int AdminId, [FromHeader] string AdminToken, [FromBody] UpdateCollaboratorRequestBody body)
        {
            var result = await _mediator.Send(new UpdateCollaboratorRequest(AdminId, AdminToken, body.UserId, body.WorkspaceId, body.CanRead, body.CanDelete, body.CanModify));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpDelete("{UserId}/{WorkspaceId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> DeleteCollaboratorAsync(int UserId, int WorkspaceId, [FromHeader] int AdminId, [FromHeader] string AdminToken)
        {
            var result = await _mediator.Send(new DeleteCollaboratorByWorkspaceAndUserIdsRequest(AdminId, AdminToken, UserId, WorkspaceId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }
    }
}
