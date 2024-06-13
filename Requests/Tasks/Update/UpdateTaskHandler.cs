using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Task.Update
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskRequest, Result<TaskDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public UpdateTaskHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<TaskDTO>> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
        {
            var task = await _db.Tasks.AsNoTracking().SingleAsync(m => m.Id == request.Id, cancellationToken);
            int previousOrder = task.Order;
            var updatedTask = _mapper.Map(request, task);


            var tasks = await _db.Tasks.Where(task => task.ListId == request.ListId && task.Id != request.Id).OrderBy(task => task.Order).ToListAsync();
            if (previousOrder != request.Order && tasks.Count() != 0)
            {
                var order = tasks.Max(task => task.Order);

                if (request.Order <= order)
                {
                    tasks.Insert(request.Order, updatedTask);

                    for (int i = 0; i < tasks.Count(); i++) tasks[i].Order = i;
                }
                //update all tasks order
            }

            _db.Update(updatedTask);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TaskDTO>(updatedTask);
        }
    }
}