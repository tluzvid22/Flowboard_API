using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Task.Get
{

    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdRequest, Result<TaskDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetTaskByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<TaskDTO>> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
        {
            var task = await _db.Tasks.Include(user => user.Files).AsNoTracking().FirstOrDefaultAsync(task => task.Id == request.TaskId, cancellationToken);

            return _mapper.Map<TaskDTO>(task);
        }
    }
}