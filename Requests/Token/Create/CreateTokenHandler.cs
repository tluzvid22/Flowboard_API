using API.DTOs;
using API.Requests.Token.Delete;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.Token.Create
{
    public class CreateTokenHandler : IRequestHandler<CreateTokenRequest, Result<TokenDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateTokenHandler(FlowboardContext db, IMapper mapper, IMediator mediator)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result<TokenDTO>> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTokenByUserIdRequest(request.UserId));

            var token = _mapper.Map<Data.Entities.Token>(request);

            await _db.Tokens.AddAsync(token, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TokenDTO>(token);   
        }
    }
}