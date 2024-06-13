using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

            if (await _db.Lists.Where(list => list.WorkspaceId == request.WorkspaceId).AnyAsync())
            {
                var order = _db.Lists.Where(list => list.WorkspaceId == request.WorkspaceId).Max(list => list.Order);
                list.Order = order + 1;
            }

            await _db.Lists.AddAsync(list, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ListDTO>(list);
        }
    }
}