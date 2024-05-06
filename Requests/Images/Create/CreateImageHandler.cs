using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.Images.Create
{
    public class CreateImageHandler : IRequestHandler<CreateImageRequest, Result<ImageDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public CreateImageHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ImageDTO>> Handle(CreateImageRequest request, CancellationToken cancellationToken)
        {
            var image = _mapper.Map<Image>(request);

            await _db.Image.AddAsync(image, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ImageDTO>(image);
        }
    }
}