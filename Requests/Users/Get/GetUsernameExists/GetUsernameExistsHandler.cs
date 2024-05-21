using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Users.Get
{

    public class GetUsernameExistsHandler : IRequestHandler<GetUsernameExistsRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetUsernameExistsHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(GetUsernameExistsRequest request, CancellationToken cancellationToken)
        {
            var username = await _db.Users
                .AsNoTracking()
                .AnyAsync(user => user.Username.ToLower().Trim() == request.Username.ToLower().Trim());            

            return username;
        }
    }
}