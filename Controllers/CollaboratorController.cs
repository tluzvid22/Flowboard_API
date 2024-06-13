using API.DTOs;
using API.Helpers;
using API.Requests.Collaborators.Create;
using API.Requests.Collaborators.Delete;
using API.Requests.Collaborators.Get;
using API.Requests.Collaborators.Update;
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

        [HttpGet("{WorkspaceId}/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RequestDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetCollaboratorByWorkspaceIdAsync(int WorkspaceId, int UserId, [FromHeader] string UserToken)
        {
            var result = await _mediator.Send(new GetCollaboratorsByWorkspaceIdRequest(WorkspaceId, UserId, UserToken));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RequestDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateCollaboratorAsync(CreateCollaboratorRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? Results.Created("/collaborator", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RequestDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> UpdateCollaboratorAsync(UpdateCollaboratorRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpDelete("{UserId}/{WorkspaceId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RequestDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> DeleteCollaboratorAsync(int UserId, int WorkspaceId, [FromHeader] int AdminId, [FromHeader] string AdminToken)
        {
            var result = await _mediator.Send(new DeleteCollaboratorByWorkspaceAndUserIdsRequest(AdminId, AdminToken, UserId, WorkspaceId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }
    }
}
