using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.List.Get
{

    public class GetListByWorkspaceIdHandler : IRequestHandler<GetListByWorkspaceIdRequest, Result<ListDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetListByWorkspaceIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ListDTO[]>> Handle(GetListByWorkspaceIdRequest request, CancellationToken cancellationToken)
        {
            var tasks = await _db.Lists.Where(f => f.WorkspaceId == request.WorkspaceId).OrderBy(f => f.Order).ToListAsync();

            return _mapper.Map<ListDTO[]>(tasks);
        }
    }
}