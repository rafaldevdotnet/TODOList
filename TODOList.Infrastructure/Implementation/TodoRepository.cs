using Microsoft.EntityFrameworkCore;
using TODOList.Application.Interfaces;
using TODOList.Domain.Entities;

namespace TODOList.Infrastructure.Implementation
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _dbContext;

        public TodoRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            return await _dbContext.Todos.ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            return await _dbContext.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Todo>> GetByDatesAsync(DateTime dateFrom, DateTime dateTo)
        {
            return await _dbContext.Todos.Where(x => x.ExpiryDate >= dateFrom && x.ExpiryDate <= dateTo).ToListAsync();
        }

        public async Task<int> AddAsync(Todo todo)
        {
            var result = await _dbContext.Todos.AddAsync(todo);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<bool> UpdateAsync(Todo todo)
        {
            _dbContext.Todos.Update(todo);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todo = await _dbContext.Todos.FindAsync(id);
            if (todo != null)
            {
                _dbContext.Todos.Remove(todo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
