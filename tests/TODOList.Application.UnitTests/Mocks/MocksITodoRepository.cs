using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Domain.Entities;

namespace TODOList.Application.UnitTests.Mocks
{
    public class MocksITodoRepository : ITodoRepository
    {
        private List<Todo> ltodo = new List<Todo>();
        public Task<int> AddAsync(Todo todo)
        {
            int id = ltodo.Count + 1;
            todo.Id = id;
            ltodo.Add(todo);
            return Task.FromResult(id);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Todo>> GetAllAsync()
        {
            return Task.FromResult(ltodo);
        }

        public Task<List<Todo>> GetByDatesAsync(DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }

        public Task<Todo?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Todo todo)
        {
            throw new NotImplementedException();
        }
    }
}
