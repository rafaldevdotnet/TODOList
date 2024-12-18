﻿using MediatR;
using TODOList.Application.Interfaces;
using TODOList.Domain.Entities;

namespace TODOList.Application.TODO.Queries
{
    public class GetTodoByIDQuery(int id) : IRequest<Todo>
    {
        public int Id { get; } = id;
    }

    public class GetTodoByIDQueryHandler(ITodoRepository todoRepository) : IRequestHandler<GetTodoByIDQuery, Todo>
    {
        public async Task<Todo> Handle(GetTodoByIDQuery request, CancellationToken cancellationToken)
        {
            return await todoRepository.GetByIdAsync(request.Id);
            
        }
    }
}
