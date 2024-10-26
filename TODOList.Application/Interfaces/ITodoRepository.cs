using TODOList.Domain.Entities;

namespace TODOList.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task<List<Todo>> GetByDatesAsync(DateTime dateFrom, DateTime dateTo);
        Task<int> AddAsync(Todo todo);
        Task<bool> UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(int id);
        
    }
}
