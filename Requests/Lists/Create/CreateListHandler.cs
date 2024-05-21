using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.List.Create
{
    public class CreateListHandler : IRequestHandler<CreateListRequest, Result<ListDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public CreateListHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ListDTO>> Handle(CreateListRequest request, CancellationToken cancellationToken)
        {
            var list = _mapper.Map<Data.Entities.List>(request);

            await _db.Lists.AddAsync(list, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ListDTO>(list);
        }
    }
}