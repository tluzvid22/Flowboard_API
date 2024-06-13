using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Collaborators.Update
{
    public class UpdateCollaboratorHandler : IRequestHandler<UpdateCollaboratorRequest, Result<CollaboratorDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public UpdateCollaboratorHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
         
        public async Task<Result<CollaboratorDTO>> Handle(UpdateCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var collaborator = await _db.Collaborator.SingleAsync(
            c=> c.WorkspaceId == request.WorkspaceId && c.UserId == request.UserId,
            cancellationToken);
            var updatedCollaborator = _mapper.Map(request, collaborator);

            _db.Update(updatedCollaborator);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CollaboratorDTO>(updatedCollaborator);
        }
    }
}