using API.DTOs;
using API.Helpers;
using API.Requests.List.Create;
using API.Requests.List.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ListController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ListDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateListAsync(CreateListRequest list)
        {
            var result = await _mediator.Send(list);
            
            return result.IsSuccess ? Results.Created("/list", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("workspace/{WorkspaceId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetListsByWorkspaceIdAsync(int WorkspaceId)
        {
            var result = await _mediator.Send(new GetListByWorkspaceIdRequest(WorkspaceId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("{ListId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetListById(int ListId)
        {
            var result = await _mediator.Send(new GetListByIdRequest(ListId));

            return result.IsSuccess ? (result.Value != null ? Results.Ok(result.Value) : Results.NotFound(null)) : result.Errors.ToBadRequest();
        }

    }
}
