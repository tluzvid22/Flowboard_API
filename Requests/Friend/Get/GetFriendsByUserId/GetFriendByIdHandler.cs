using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Requests.Friend.Get
{

    public class GetFriendByIdHandler : IRequestHandler<GetFriendByIdRequest, Result<FriendDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetFriendByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<FriendDTO[]>> Handle(GetFriendByIdRequest request, CancellationToken cancellationToken)
        {
            var friend = await _db.Friends
                .Include(f => f.User1)
                .ThenInclude(u1 => u1.Image)
                .Include(f => f.User2)
                .ThenInclude(u2 => u2.Image) 
                .Where(friend => friend.User1Id == request.UserId || friend.User2Id == request.UserId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            //set to no tracking (no modifications will be made in the entity) 👇

            friend.ForEach(f =>
            {
                if (f.User1Id == request.UserId) f.User1 = null!;
                else f.User2 = null!;
            });
            //to let know the mapper which user is the one to map

            return _mapper.Map<FriendDTO[]>(friend);
        }
    }
}