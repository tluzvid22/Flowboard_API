using API.DTOs;
using AutoMapper;
using Data.Setup;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Requests.List.Update
{
    public class UpdateListHandler : IRequestHandler<UpdateListRequest, Result<ListDTO>>
    {

        private readonly FlowboardContext _db;
        private readonly IMapper _mapper;

        public UpdateListHandler(FlowboardContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Result<ListDTO>> Handle(UpdateListRequest request, CancellationToken cancellationToken)
        {
            var list = await _db.Lists.AsNoTracking().SingleAsync(m => m.Id == request.Id, cancellationToken);
            int previousOrder = list.Order;
            var updatedList = _mapper.Map(request, list);


            var lists = await _db.Lists.Where(list => list.WorkspaceId == request.WorkspaceId && list.Id != request.Id).OrderBy(list => list.Order).ToListAsync();
            if (previousOrder != request.Order && lists.Count() != 0)
            {
                var order = lists.Max(list => list.Order);

                if (request.Order <= order)
                {
                    lists.Insert(request.Order, updatedList);

                    for (int i = 0; i < lists.Count(); i++) lists[i].Order = i;
                }
                //update all lists order
            }

            _db.Update(updatedList);
            await _db.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ListDTO>(updatedList);
        }
    }
}