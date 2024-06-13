using API.DTOs;
using API.Helpers;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Files.Get
{

    public class GetFileInfoByIdHandler : IRequestHandler<GetFileInfoByIdRequest, Result<FileDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetFileInfoByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<FileDTO>> Handle(GetFileInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var file = await _db.Files
                .Select(i => new { i.Name, i.FileType, i.Id, i.CreatedAt, i.UpdatedAt, i.TaskId })
                .Where(i => i.Id == request.FileId)
                .FirstOrDefaultAsync(cancellationToken);


            return _mapper.Map<FileDTO>(
                new Data.Entities.Files(){ Id = file.Id, FileType=file.FileType, 
                CreatedAt=file.CreatedAt, 
                Name=file.Name, UpdatedAt=file.UpdatedAt, TaskId=file.TaskId, File= null});
        }
    }
}