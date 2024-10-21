using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Domain.Entities;

namespace TODOList.Application.TODO.Queries
{
    public class GetAllTodosQuery : IRequest<List<Todo>>
    {
    }

    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<Todo>>
    {
        private readonly ITodoRepository _todoRepository;
        public GetAllTodosQueryHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Task<List<Todo>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            return _todoRepository.GetAllAsync();
        } 
    }
}
