using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Images.Get
{

    public class GetImagesHandler : IRequestHandler<GetImagesRequest, Result<ImageDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetImagesHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ImageDTO[]>> Handle(GetImagesRequest request, CancellationToken cancellationToken)
        {
            var images = await _db.Image
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            return _mapper.Map<ImageDTO[]>(images);
        }
    }
}