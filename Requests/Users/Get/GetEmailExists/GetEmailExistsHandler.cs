using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Users.Get
{

    public class GetEmailExistsHandler : IRequestHandler<GetEmailExistsRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetEmailExistsHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(GetEmailExistsRequest request, CancellationToken cancellationToken)
        {
            var email = await _db.Users
                .AsNoTracking()
                .AnyAsync(user => user.Email.ToLower().Trim() == request.Email.ToLower().Trim());            

            return email;
        }
    }
}