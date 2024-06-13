using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Data.Entities;

namespace API.Requests.Collaborators.Create
{

    public class CreateCollaboratorHandler : IRequestHandler<CreateCollaboratorRequest, Result<CollaboratorDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public CreateCollaboratorHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<CollaboratorDTO>> Handle(CreateCollaboratorRequest request, CancellationToken cancellationToken)
        {
            var collaborator = _mapper.Map<Collaborator>(request);

            await _db.Collaborator.AddAsync(collaborator, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CollaboratorDTO>(collaborator);
        }
    }
}