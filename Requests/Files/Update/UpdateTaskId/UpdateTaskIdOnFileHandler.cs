using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Files.Create
{
    public class UpdateTaskIdOnFileHandler : IRequestHandler<UpdateTaskIdOnFileRequest, Result<FileDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public UpdateTaskIdOnFileHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<FileDTO>> Handle(UpdateTaskIdOnFileRequest request, CancellationToken cancellationToken)
        {
            var file = await _db.Files.SingleAsync(f => f.Id == request.Id, cancellationToken);

            _mapper.Map(request, file);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FileDTO>(file);
        }
    }
}