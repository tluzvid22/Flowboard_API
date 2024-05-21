using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.File.Get
{

    public class GetFileByTaskIdHandler : IRequestHandler<GetFileByTaskIdRequest, Result<FileDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetFileByTaskIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<FileDTO[]>> Handle(GetFileByTaskIdRequest request, CancellationToken cancellationToken)
        {
            var file = await _db.Files.Select(i => new { i.Name, i.FileType, i.Id, i.CreatedAt, i.CreatedBy, i.UpdatedAt, i.UpdatedBy, i.TaskId })
                .Where(f => f.TaskId == request.TaskId)
                .ToListAsync();

            var files = new List<Data.Entities.Files>();

            file.ForEach(file => {
                files.Add(new Data.Entities.Files()
                {
                    Id = file.Id,
                    FileType = file.FileType,
                    CreatedAt = file.CreatedAt,
                    CreatedBy = file.CreatedBy,
                    Name = file.Name,
                    UpdatedAt = file.UpdatedAt,
                    UpdatedBy = file.UpdatedBy,
                    TaskId = file.TaskId,
                    File = null
                });
            });

            return _mapper.Map<FileDTO[]>(files);
        }
    }
}