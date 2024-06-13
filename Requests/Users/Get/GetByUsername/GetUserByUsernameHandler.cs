using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Users.Get
{

    public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameRequest, Result<PublicUserDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetUserByUsernameHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<PublicUserDTO[]>> Handle(GetUserByUsernameRequest request, CancellationToken cancellationToken)
        {
            var searchTerms = request.Username.ToLower() == "empty" ? "" : request.Username.ToLower();
            var user = await _db.Users
                .Where(user => user.Username.ToLower().Contains(searchTerms))
                .Include(user => user.Image)
                .Take(10)
                .ToListAsync(cancellationToken);

            return _mapper.Map<PublicUserDTO[]>(user);
        }
    }
}