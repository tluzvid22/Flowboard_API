using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Token.Delete
{

    public class DeleteTokenByTokenValueHandler : IRequestHandler<DeleteTokenByTokenValueRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public DeleteTokenByTokenValueHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(DeleteTokenByTokenValueRequest request, CancellationToken cancellationToken)
        {
            var token = await _db.Tokens
                .AsNoTracking()
                .FirstOrDefaultAsync(token => token.Value == request.Token, cancellationToken);

            if (token != null)
            {
                _db.Tokens.Remove(token);
                await _db.SaveChangesAsync();
            }

            return true;
        }
    }
}