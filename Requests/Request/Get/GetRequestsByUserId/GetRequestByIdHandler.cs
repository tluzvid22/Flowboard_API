using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Requests.Request.Get
{

    public class GetRequestByIdHandler : IRequestHandler<GetRequestByIdRequest, Result<RequestDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetRequestByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<RequestDTO[]>> Handle(GetRequestByIdRequest request, CancellationToken cancellationToken)
        {
            var requestEntity = await _db.Requests
                .Include(f => f.Sender)
                .ThenInclude(u1 => u1.Image)
                .Include(f => f.Receiver)
                .ThenInclude(u2 => u2.Image)
                .Where(r => r.SenderId == request.UserId || r.ReceiverId == request.UserId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            //set to no tracking (no modifications will be made in the entity) 👇

            requestEntity.ForEach(r =>
            {
                if (r.SenderId == request.UserId) r.Sender = null!;
                else r.Receiver = null!;
            });
            //to let know the mapper which user is the one to map

            return _mapper.Map<RequestDTO[]>(requestEntity);
        }
    }
}