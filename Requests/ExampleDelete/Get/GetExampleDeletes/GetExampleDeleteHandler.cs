using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.ExampleDelete.Get
{

    public class GetExampleDeleteHandler : IRequestHandler<GetExampleDeleteRequest, Result<UserDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetExampleDeleteHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<UserDTO[]>> Handle(GetExampleDeleteRequest request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}