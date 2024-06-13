using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Collaborators.Delete
{

    public class DeleteCollaboratorByWorkspaceAndUserIdsHandler : IRequestHandler<DeleteCollaboratorByWorkspaceAndUserIdsRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public DeleteCollaboratorByWorkspaceAndUserIdsHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(DeleteCollaboratorByWorkspaceAndUserIdsRequest request, CancellationToken cancellationToken)
        {
            var collaboratorEntity = await _db.Collaborator
                .AsNoTracking()
                .FirstOrDefaultAsync(collaborator => collaborator.UserId == request.UserId && collaborator.WorkspaceId == request.WorkspaceId, cancellationToken);


            if (collaboratorEntity != null)
            {
                _db.Collaborator.Remove(collaboratorEntity);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}