using API.DTOs;
using API.Helpers;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Requests.Files.Get
{

    public class GetFileContentByIdHandler : IRequestHandler<GetFileContentByIdRequest, Result<FileContentResult>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetFileContentByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<FileContentResult>> Handle(GetFileContentByIdRequest request, CancellationToken cancellationToken)
        {
            var file = await _db.Files.FindAsync([request.FileId], cancellationToken);

            if (file?.File == null) {
                return null;
            }

            return new FileContentResult(file.File, MediaTypeHelper.GetMediaType(file.FileType));
        }
    }
}