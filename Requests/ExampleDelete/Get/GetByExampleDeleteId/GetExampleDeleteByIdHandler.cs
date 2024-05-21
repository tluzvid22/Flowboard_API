using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.ExampleDelete.Get
{

    public class GetExampleDeleteByIdHandler : IRequestHandler<GetExampleDeleteByIdRequest, Result<UserDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetExampleDeleteByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<UserDTO>> Handle(GetExampleDeleteByIdRequest request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}