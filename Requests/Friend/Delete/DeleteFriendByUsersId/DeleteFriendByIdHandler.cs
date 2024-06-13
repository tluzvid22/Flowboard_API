using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Friend.Delete
{

    public class DeleteFriendByIdHandler : IRequestHandler<DeleteFriendByIdRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public DeleteFriendByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(DeleteFriendByIdRequest request, CancellationToken cancellationToken)
        {
            var friend = await _db.Friends
                .AsNoTracking()
                .FirstOrDefaultAsync(friend => (friend.User1Id == request.User1Id && friend.User2Id == request.User2Id)
                    || (friend.User2Id == request.User1Id && friend.User1Id == request.User2Id));


            if (friend != null)
            {
                _db.Friends.Remove(friend);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}