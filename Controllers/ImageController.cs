using API.DTOs;
using API.Helpers;
using API.Requests.Images.Create;
using API.Requests.Images.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ImageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> CreateImageAsync(IFormFile file)
        {
            var result = await _mediator.Send(new CreateImageRequest(file));
            
            return result.IsSuccess ? Results.Created("/user", result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageDTO[]>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetImagesAsync()
        {
            var result = await _mediator.Send(new GetImagesRequest());

            return result.IsSuccess ? Results.Ok(result.Value) : result.Errors.ToBadRequest();
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IResult> GetImagesById(int Id)
        {
            var result = await _mediator.Send(new GetImageByIdRequest(Id));

            return result.IsSuccess ? (result.Value != null ? Results.Ok(result.Value) : Results.NotFound(null)) : result.Errors.ToBadRequest();
        }

    }
}
