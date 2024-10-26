using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Domain.Entities;

namespace TODOList.Application.UnitTests.Mocks
{
    public static class MocksRepositories
    {
        public static Mock<ITodoRepository> GetTodoRepository()
        {
            var repo = new List<Todo>()
            {
                new Todo()
                {
                    Id = 1,
                    Title = "Test 1",
                    Description = "Test 1 Description",
                    ExpiryDate = DateTime.Now.AddDays(1)
                },
                new Todo()
                {
                    Id = 2,
                    Title = "Test 2",
                    Description = "Test 2 Description",
                    ExpiryDate = DateTime.Now.AddDays(2)
                }
            };
            // Create a new mock of ITodoRepository
            var mock = new Mock<ITodoRepository>();

            mock.Setup(x => x.GetAllAsync()).ReturnsAsync(repo);

            mock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => repo.FirstOrDefault(x => x.Id == id));

            mock.Setup(x => x.GetByDatesAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync((DateTime dateFrom, DateTime dateTo) => repo.Where(x => x.ExpiryDate >= dateFrom && x.ExpiryDate <= dateTo.AddDays(1).AddMicroseconds(-1)).ToList());

            mock.Setup(x => x.AddAsync(It.IsAny<Todo>())).ReturnsAsync((Todo todo) =>
            {
                int id = repo.Max(x => x.Id) + 1;
                todo.Id = id;
                repo.Add(todo);
                return id;
            });

            mock.Setup(x => x.UpdateAsync(It.IsAny<Todo>())).ReturnsAsync((Todo todo) =>
            {
                var existing = repo.FirstOrDefault(x => x.Id == todo.Id);
                if (existing == null)
                {
                    return false;
                }

                existing.Title = todo.Title;
                existing.Description = todo.Description;
                existing.ExpiryDate = todo.ExpiryDate;

                return true;
            });

            mock.Setup(x => x.DeleteAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                var existing = repo.FirstOrDefault(x => x.Id == id);
                if (existing == null)
                {
                    return false;
                }

                repo.Remove(existing);

                return true;
            });

            return mock;
        }
    }
}
