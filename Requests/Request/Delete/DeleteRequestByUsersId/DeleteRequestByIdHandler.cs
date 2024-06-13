using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Request.Delete
{

    public class DeleteRequestByIdHandler : IRequestHandler<DeleteRequestByIdRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public DeleteRequestByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(DeleteRequestByIdRequest request, CancellationToken cancellationToken)
        {
            var requestEntity = await _db.Requests
                .AsNoTracking()
                .FirstOrDefaultAsync(r => (r.SenderId == request.SenderId && r.ReceiverId == request.ReceiverId)
                    || (r.ReceiverId == request.SenderId && r.SenderId == request.ReceiverId));


            if (requestEntity != null)
            {
                _db.Requests.Remove(requestEntity);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}