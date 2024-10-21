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
    public class GetTodoByDatesQuery : IRequest<List<Todo>>
    {
        public GetTodoByDatesQuery(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class GetTodoByDatesQueryHandler : IRequestHandler<GetTodoByDatesQuery, List<Todo>>
    {
        private readonly ITodoRepository _todoRepository;
        public GetTodoByDatesQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<Todo>> Handle(GetTodoByDatesQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetByDatesAsync(request.DateFrom, request.DateTo);
            return todos;
        }
    }
}
