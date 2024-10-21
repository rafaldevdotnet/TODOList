using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.TODO.DTOs;
using TODOList.Domain.Entities;

namespace TODOList.Application.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task<List<Todo>> GetByDatesAsync(DateTime dateFrom, DateTime dateTo);
        Task<int> AddAsync(CreateTodoDto todo);
        Task<bool> UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(int id);
        
    }
}
