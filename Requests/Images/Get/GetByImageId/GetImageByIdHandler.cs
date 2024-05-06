using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.Images.Get
{

    public class GetImageByIdHandler : IRequestHandler<GetImageByIdRequest, Result<ImageDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetImageByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ImageDTO>> Handle(GetImageByIdRequest request, CancellationToken cancellationToken)
        {
            var image = await _db.Image.FindAsync([request.Id], cancellationToken);

            return _mapper.Map<ImageDTO>(image);
        }
    }
}