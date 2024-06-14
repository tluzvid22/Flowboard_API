using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API.Requests.UserTasks.Get
{

    public class GetUserTasksHandler : IRequestHandler<GetUserTasksRequest, Result<UserTaskDTO[]>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public GetUserTasksHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<UserTaskDTO[]>> Handle(GetUserTasksRequest request, CancellationToken cancellationToken)
        {
            var userTaskEntity = await _db.UserTask
                .Include(c => c.User)
                .ThenInclude(u => u.Image)
                .Include(c => c.Task)
                .Where(c => c.TaskId == request.TaskId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<UserTaskDTO[]>(userTaskEntity);
        }
    }
}