using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Workspace.Get
{

    public class GetWorkspaceByIdHandler : IRequestHandler<GetWorkspaceByIdRequest, Result<WorkspaceDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetWorkspaceByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<WorkspaceDTO>> Handle(GetWorkspaceByIdRequest request, CancellationToken cancellationToken)
        {
            var list = await _db.Workspaces
                .AsNoTracking()
                .FirstOrDefaultAsync(workspace => workspace.Id == request.WorkspaceId && workspace.UserId == request.UserId && workspace.User.Token.Value == request.Token, cancellationToken);

            return _mapper.Map<WorkspaceDTO>(list);
        }
    }
}