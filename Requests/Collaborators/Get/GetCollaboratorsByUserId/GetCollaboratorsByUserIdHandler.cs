using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Requests.Collaborators.Get
{

    public class GetCollaboratorsByUserIdHandler : IRequestHandler<GetCollaboratorsByUserIdRequest, Result<CollaboratorDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetCollaboratorsByUserIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<CollaboratorDTO[]>> Handle(GetCollaboratorsByUserIdRequest request, CancellationToken cancellationToken)
        {
            var collaboratorEntity = await _db.Collaborator
                .Where(c => c.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<CollaboratorDTO[]>(collaboratorEntity);
        }
    }
}