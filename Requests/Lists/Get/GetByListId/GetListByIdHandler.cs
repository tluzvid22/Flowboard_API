using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.List.Get
{

    public class GetListByIdHandler : IRequestHandler<GetListByIdRequest, Result<ListDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetListByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ListDTO>> Handle(GetListByIdRequest request, CancellationToken cancellationToken)
        {
            var list = await _db.Lists.Include(list => list.Tasks).AsNoTracking().FirstOrDefaultAsync(list => list.Id == request.ListId, cancellationToken);

            return _mapper.Map<ListDTO>(list);
        }
    }
}