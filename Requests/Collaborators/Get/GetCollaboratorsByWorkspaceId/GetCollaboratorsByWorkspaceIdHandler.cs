using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Requests.Collaborators.Get
{

    public class GetCollaboratorsByWorkspaceIdHandler : IRequestHandler<GetCollaboratorsByWorkspaceIdRequest, Result<CollaboratorDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetCollaboratorsByWorkspaceIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<CollaboratorDTO[]>> Handle(GetCollaboratorsByWorkspaceIdRequest request, CancellationToken cancellationToken)
        {
            var collaboratorEntity = await _db.Collaborator
                .Include(c => c.User)
                .ThenInclude(u => u.Image)
                .Include(c => c.Workspace)
                .Where(c => c.WorkspaceId == request.WorkspaceId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<CollaboratorDTO[]>(collaboratorEntity);
        }
    }
}