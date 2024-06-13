using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.List.Delete
{

    public class DeleteListByIdHandler : IRequestHandler<DeleteListByIdRequest, Result<bool>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public DeleteListByIdHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<bool>> Handle(DeleteListByIdRequest request, CancellationToken cancellationToken)
        {
            var list = await _db.Lists
                .Include(list => list.Tasks)
                .ThenInclude(task => task.Files)
                .AsNoTracking()
                .FirstOrDefaultAsync((list) => list.Id == request.ListId);


            if (list != null)
            {
                var files = list.Tasks.SelectMany(task => task.Files).ToArray();
                _db.Files.RemoveRange(files);
                _db.Lists.Remove(list);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}