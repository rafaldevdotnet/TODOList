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
    public class CreateNewTodoCommand(CreateTodoDto todo) : IRequest<int>
    {
        public CreateTodoDto Todo { get; } = todo;
    }

    public class CreateNewTodoCommandHandler(ITodoRepository todoRepository) : IRequestHandler<CreateNewTodoCommand, int>
    {
        public async Task<int> Handle(CreateNewTodoCommand request, CancellationToken cancellationToken)
        {
            int id = await todoRepository.AddAsync(new Todo()
            {
                Title = request.Todo.Title,
                Description = request.Todo.Description,
                ExpiryDate = request.Todo.ExpiryDate
            });

            return id;
        }
    }
}
