using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Application.TODO.DTOs;
using TODOList.Domain.Entities;

namespace TODOList.Application.TODO.Commands
{
    public class CreateNewTodoCommand : IRequest<int>
    {
        public CreateNewTodoCommand(CreateTodoDto todo)
        {
            Todo = todo;
        }
        public CreateTodoDto Todo { get; set; }
    }

    public class CreateNewTodoCommandHandler : IRequestHandler<CreateNewTodoCommand, int>
    {
        private readonly ITodoRepository _todoRepository;
        public CreateNewTodoCommandHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<int> Handle(CreateNewTodoCommand request, CancellationToken cancellationToken)
        {
            int id = await _todoRepository.AddAsync(request.Todo);

            return id;
        }
    }
}
