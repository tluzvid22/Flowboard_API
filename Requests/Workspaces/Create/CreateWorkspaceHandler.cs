using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.Workspace.Create
{
    public class CreateWorkspaceHandler : IRequestHandler<CreateWorkspaceRequest, Result<WorkspaceDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public CreateWorkspaceHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<WorkspaceDTO>> Handle(CreateWorkspaceRequest request, CancellationToken cancellationToken)
        {
            var workspace = _mapper.Map<Data.Entities.Workspace>(request);

            workspace.Collaborator.Add(new Collaborator() { WorkspaceId = workspace.Id, UserId = request.UserId, IsAdmin = true, 
                CanDelete = true, CanModify = true, CanRead = true});

            await _db.Workspaces.AddAsync(workspace, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<WorkspaceDTO>(workspace);
        }
    }
}