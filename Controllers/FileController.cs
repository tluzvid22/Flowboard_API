using API.DTOs;
using API.Helpers;
using API.Requests.Files.Create;
using API.Requests.File.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using API.Requests.Files.Get;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public FileController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FileDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateFileAsync(string name, string filetype, IFormFile data)
        {
            var result = await _mediator.Send(new CreateFileRequest(name, filetype, data));
            
            return result.IsSuccess ? Results.Created("/file", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("task/{TaskId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FileDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetFilesByTaskIdAsync(int TaskId)
        {
            var result = await _mediator.Send(new GetFileByTaskIdRequest(TaskId));

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("content/{FileId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFileContentById(int FileId)
        {
            var result = await _mediator.Send(new GetFileContentByIdRequest(FileId));

            return result != null ? (result.Value != null ? result.Value : BadRequest(result.Errors.ToBadRequest())) : NotFound();
        }


        [HttpGet("info/{FileId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetFileInfoById(int FileId)
        {
            var result = await _mediator.Send(new GetFileInfoByIdRequest(FileId));

            return result.IsSuccess ? (result.Value != null ? Results.Ok(result.Value) : Results.NotFound(null)) : result.Errors.ToBadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> UpdateTaskIdOnFileAsync([FromBody] UpdateTaskIdOnFileRequest request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess ? (result.Value != null ? Results.Ok(result.Value) : Results.NotFound(null)) : result.Errors.ToBadRequest();
        }


    }
}
