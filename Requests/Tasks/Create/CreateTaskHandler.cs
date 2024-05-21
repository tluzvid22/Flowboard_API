using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;

namespace API.Requests.Task.Create
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskRequest, Result<TaskDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public CreateTaskHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<TaskDTO>> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<Data.Entities.Task>(request);

            await _db.Tasks.AddAsync(task, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TaskDTO>(task);
        }
    }
}