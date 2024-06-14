using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Data.Entities;

namespace API.Requests.UserTasks.Create
{

    public class CreateUserTaskHandler : IRequestHandler<CreateUserTaskRequest, Result<UserTaskDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public CreateUserTaskHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<UserTaskDTO>> Handle(CreateUserTaskRequest request, CancellationToken cancellationToken)
        {
            var userTask = _mapper.Map<UserTask>(request);

            await _db.UserTask.AddAsync(userTask, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserTaskDTO>(userTask);
        }
    }
}