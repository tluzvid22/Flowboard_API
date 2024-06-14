using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.UserTasks.Delete
{

    public class DeleteUserTaskHandler : IRequestHandler<DeleteUserTaskRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public DeleteUserTaskHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(DeleteUserTaskRequest request, CancellationToken cancellationToken)
        {
            var userTaskEntity = await _db.UserTask
                .AsNoTracking()
                .FirstOrDefaultAsync(userTask => userTask.UserId == request.UserId && userTask.TaskId == request.TaskId, cancellationToken);

            if (userTaskEntity != null)
            {
                _db.UserTask.Remove(userTaskEntity);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}