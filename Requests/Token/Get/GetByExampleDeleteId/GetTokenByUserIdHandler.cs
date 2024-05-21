using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Token.Get
{

    public class GetTokenByUserIdHandler : IRequestHandler<GetTokenByUserIdRequest, Result<TokenDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetTokenByUserIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<TokenDTO[]>> Handle(GetTokenByUserIdRequest request, CancellationToken cancellationToken)
        {
            var token = await _db.Tokens.Where(token => token.UserId == request.UserId).ToListAsync(cancellationToken);

            return _mapper.Map<TokenDTO[]>(token);
        }
    }
}