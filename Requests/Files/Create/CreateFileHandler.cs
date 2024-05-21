using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.Files.Create
{
    public class CreateFileHandler : IRequestHandler<CreateFileRequest, Result<FileDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public CreateFileHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<FileDTO>> Handle(CreateFileRequest request, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<Data.Entities.Files>(request);

            await _db.Files.AddAsync(file, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FileDTO>(file);
        }
    }
}