using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.File.Get
{

    public class GetFileByIdHandler : IRequestHandler<GetFileByIdRequest, Result<FileDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetFileByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<FileDTO>> Handle(GetFileByIdRequest request, CancellationToken cancellationToken)
        {
            var file = await _db.Files.FindAsync([request.FileId], cancellationToken);

            return _mapper.Map<FileDTO>(file);
        }
    }
}