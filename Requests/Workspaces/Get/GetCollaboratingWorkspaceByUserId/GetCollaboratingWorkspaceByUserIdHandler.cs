using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Workspace.Get
{

    public class GetCollaboratingWorkspaceByUserIdHandler : IRequestHandler<GetWorkspaceByUserIdRequest, Result<WorkspaceDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetCollaboratingWorkspaceByUserIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<WorkspaceDTO[]>> Handle(GetWorkspaceByUserIdRequest request, CancellationToken cancellationToken)
        {
            var workspaces = await _db.Collaborator
                .Include(collaborator => collaborator.User)
                .Include(collaborator => collaborator.Workspace)
                .ThenInclude(workspace => workspace.Lists)
                .Where(collaborator => collaborator.UserId == request.UserId && !collaborator.IsAdmin && collaborator.User.Token.Value == request.Token)//workspace.User.Token.Value == request.Token)
                .Select(collaborator => collaborator.Workspace)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<WorkspaceDTO[]>(workspaces);
        }
    }
}