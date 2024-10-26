using MediatR;
using TODOList.Application.Interfaces;
using TODOList.Domain.Entities;

namespace TODOList.Application.TODO.Commands
{
    public class UpdateTodoCommand(Todo todo, Todo updateTodo) : IRequest<bool>
    {
        public Todo Todo { get; } = todo;
        public Todo UpdateTodo { get; } = updateTodo;
    }

    public class UpdateTodoHandlerCommand(ITodoRepository todoRepository) : IRequestHandler<UpdateTodoCommand, bool>
    {
        public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var up = new Todo
            {
                Id = request.Todo.Id,
                Title = !string.IsNullOrWhiteSpace(request.UpdateTodo.Title) ? request.UpdateTodo.Title : request.Todo.Title,
                Description = !string.IsNullOrWhiteSpace(request.UpdateTodo.Description) ? request.UpdateTodo.Description : request.Todo.Description,
                ExpiryDate = request.UpdateTodo.ExpiryDate >= DateTime.Today ? request.UpdateTodo.ExpiryDate : request.Todo.ExpiryDate,
                PercentComplete = request.UpdateTodo.PercentComplete,
                IsDone = request.UpdateTodo.IsDone
            };
            
            
            return await todoRepository.UpdateAsync(up);
        }
    }
}
