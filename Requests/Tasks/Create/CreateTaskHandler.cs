using API.DTOs;
using AutoMapper;
using Data.Entities;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

            if (await _db.Tasks.Where(task => task.ListId == request.ListId).AnyAsync())
            {
                var order = _db.Tasks.Where(task => task.ListId == request.ListId).Max(task => task.Order);
                task.Order = order + 1;
            }

            await _db.Tasks.AddAsync(task, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TaskDTO>(task);
        }
    }
}