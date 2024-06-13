using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Workspace.Delete
{

    public class DeleteWorkspaceByIdHandler : IRequestHandler<DeleteWorkspaceByIdRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public DeleteWorkspaceByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(DeleteWorkspaceByIdRequest request, CancellationToken cancellationToken)
        {
            var workspace = await _db.Workspaces
                .Include(workspace => workspace.Collaborator)
                .Include(workspace => workspace.Lists)
                .ThenInclude(list => list.Tasks)
                .ThenInclude(task => task.Files)
                .AsNoTracking()
                .FirstOrDefaultAsync((workspace) => 
                    workspace.Id == request.WorkspaceId && workspace.UserId == request.UserId 
                    && workspace.User.Token.Value == request.Token, cancellationToken);


            if (workspace != null)
            {
                var files = ((workspace.Lists.SelectMany(list => list.Tasks.SelectMany(task => task.Files)))).ToArray();
                _db.Files.RemoveRange(files);
                var collaborators = workspace.Collaborator;
                _db.Collaborator.RemoveRange(collaborators);
                _db.Workspaces.Remove(workspace);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}