using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Users.Get
{

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, Result<UserDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<UserDTO>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Include(user => user.Image)
                .Include(user => user.Workspaces)
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);

            return _mapper.Map<UserDTO>(user);
        }
    }
}