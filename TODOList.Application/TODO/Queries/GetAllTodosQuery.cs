using MediatR;
using TODOList.Application.Interfaces;
using TODOList.Domain.Entities;

namespace TODOList.Application.TODO.Queries
{
    public class GetAllTodosQuery : IRequest<List<Todo>>
    { }


    public class GetAllTodosQueryHandler(ITodoRepository todoRepository) : IRequestHandler<GetAllTodosQuery, List<Todo>>
    {   
        public Task<List<Todo>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            return todoRepository.GetAllAsync();
        } 
    }
}
