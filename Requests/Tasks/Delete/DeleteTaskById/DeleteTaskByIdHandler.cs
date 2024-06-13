using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.Task.Delete
{

    public class DeleteTaskByIdHandler : IRequestHandler<DeleteTaskByIdRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public DeleteTaskByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(DeleteTaskByIdRequest request, CancellationToken cancellationToken)
        {
            var task = await _db.Tasks
                .Include(task => task.Files)
                .FirstOrDefaultAsync((task) => 
                    task.Id == request.Id);


            if (task != null)
            {
                _db.Files.RemoveRange(task.Files);
                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}