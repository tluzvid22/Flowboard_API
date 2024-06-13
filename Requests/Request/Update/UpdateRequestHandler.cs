using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Request.Update
{
    public class UpdateRequestHandler : IRequestHandler<UpdateRequestRequest, Result<RequestDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public UpdateRequestHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
         
        public async Task<Result<RequestDTO>> Handle(UpdateRequestRequest request, CancellationToken cancellationToken)
        {
            var requestEntity = await _db.Requests.SingleAsync(
            r => (r.SenderId == request.SenderId && r.ReceiverId == request.ReceiverId) 
            || (r.ReceiverId == request.SenderId && r.SenderId == request.ReceiverId), 
            cancellationToken);

            requestEntity.Status = request.Status;

            _db.Update(requestEntity);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RequestDTO>(requestEntity);
        }
    }
}