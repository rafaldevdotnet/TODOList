using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Domain.Entities;

namespace TODOList.Application.TODO.Queries
{
    public class GetTodoByDatesQuery(DateTime dateFrom, DateTime dateTo) : IRequest<List<Todo>>
    {
        public DateTime DateFrom { get; } = dateFrom;
        public DateTime DateTo { get; } = dateTo;
    }

    public class GetTodoByDatesQueryHandler(ITodoRepository todoRepository) : IRequestHandler<GetTodoByDatesQuery, List<Todo>>
    {
        public async Task<List<Todo>> Handle(GetTodoByDatesQuery request, CancellationToken cancellationToken)
        {
            var todos = await todoRepository.GetByDatesAsync(request.DateFrom, request.DateTo);
            return todos;
        }
    }
}
