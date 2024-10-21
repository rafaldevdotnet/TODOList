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
    public class GetTodoByIDQuery :IRequest<Todo>
    {
        public GetTodoByIDQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class GetTodoByIDQueryHandler : IRequestHandler<GetTodoByIDQuery, Todo>
    {
        private readonly ITodoRepository _todoRepository;
        public GetTodoByIDQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Todo> Handle(GetTodoByIDQuery request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id);
            return todo;
        }
    }
}
