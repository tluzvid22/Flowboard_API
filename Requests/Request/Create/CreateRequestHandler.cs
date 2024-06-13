using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Request.Create
{

    public class CreateRequestHandler : IRequestHandler<CreateRequestRequest, Result<RequestDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public CreateRequestHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<RequestDTO>> Handle(CreateRequestRequest request, CancellationToken cancellationToken)
        {
            var users = await _db.Users.Where(user => user.Id == request.SenderId ||  user.Id == request.ReceiverId).AsNoTracking().ToListAsync(cancellationToken);
            var user1 = users.Find(user => user.Id == request.SenderId)!;
            var user2 = users.Find(user => user.Id == request.ReceiverId)!;

            var requestEntity = _mapper.Map<Data.Entities.Request>(request);
            requestEntity.Status = Data.Enums.Status.Waiting;

            await _db.Requests.AddAsync(requestEntity, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RequestDTO>(requestEntity);
        }
    }
}