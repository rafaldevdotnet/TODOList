using MediatR;
using TODOList.Application.Interfaces;

namespace TODOList.Application.TODO.Commands
{
    public class DeleteTodoCommand(int id):IRequest<bool>
    {
        public int ID { get; } = id;
    }

    public class DeleteTodoCommandHandler(ITodoRepository todoRepository) : IRequestHandler<DeleteTodoCommand, bool>
    {
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            return await todoRepository.DeleteAsync(request.ID);
        }
    }
}
