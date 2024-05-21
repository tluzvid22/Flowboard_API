using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Workspace.Get
{

    public class GetWorkspaceByUserIdHandler : IRequestHandler<GetWorkspaceByUserIdRequest, Result<WorkspaceDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetWorkspaceByUserIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<WorkspaceDTO[]>> Handle(GetWorkspaceByUserIdRequest request, CancellationToken cancellationToken)
        {
            var workspaces = await _db.Workspaces.Where(workspace => workspace.UserId == request.UserId && workspace.User.Token.Value == request.Token).ToListAsync();

            return _mapper.Map<WorkspaceDTO[]>(workspaces);
        }
    }
}