using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Users.Get
{

    public class GetUserByTokenHandler : IRequestHandler<GetUserByTokenRequest, Result<UserDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetUserByTokenHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<UserDTO[]>> Handle(GetUserByTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Where(user => user.Token.Value == request.Token)
                .ToListAsync(cancellationToken);

            return _mapper.Map<UserDTO[]>(user);
        }
    }
}