using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Task.Get
{

    public class GetTaskByListIdHandler : IRequestHandler<GetTaskByListIdRequest, Result<TaskDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetTaskByListIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<TaskDTO[]>> Handle(GetTaskByListIdRequest request, CancellationToken cancellationToken)
        {
            var tasks = await _db.Tasks.Where(f => f.ListId == request.ListId).OrderBy(f => f.Order).ToListAsync();

            return _mapper.Map<TaskDTO[]>(tasks);
        }
    }
}