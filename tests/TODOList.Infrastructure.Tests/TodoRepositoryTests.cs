using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using TODOList.Infrastructure;
using TODOList.Infrastructure.Implementation;
using TODOList.Domain.Entities;

namespace TODOList.Infrastructure.Tests
{
    public class TodoRepositoryTests
    {
        private TodoContext CreateInMemoryDbContext(string nameDB)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: nameDB)
                .Options;

            return new TodoContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTodos()
        {
            // Arrange
            var context = CreateInMemoryDbContext("DBGetAll");
            var repository = new TodoRepository(context);

            var todo1 = new Todo { Title = "Test Todo 1", Description = "Description 1", ExpiryDate = DateTime.Now, PercentComplete = 50, IsDone = false };
            var todo2 = new Todo { Title = "Test Todo 2", Description = "Description 2", ExpiryDate = DateTime.Now, PercentComplete = 100, IsDone = true };

            await repository.AddAsync(todo1);
            await repository.AddAsync(todo2);

            // Act
            var todos = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, todos.Count);
            Assert.Equal("Test Todo 1", todos[0].Title);
            Assert.Equal("Test Todo 2", todos[1].Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTodoWithGivenId()
        {
            // Arrange
            var context = CreateInMemoryDbContext("DBGetID");
            var repository = new TodoRepository(context);

            var todo = new Todo { Title = "Todo 1", Description = "Description 1", ExpiryDate = DateTime.Now, PercentComplete = 50, IsDone = false };
            var id = await repository.AddAsync(todo);

            // Act
            var fetchedTodo = await repository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(fetchedTodo);
            Assert.Equal("Todo 1", fetchedTodo.Title);
        }

        [Fact]
        public async Task GetByDatesAsync_ShouldReturnTodosWithinDateRange()
        {
            // Arrange
            var context = CreateInMemoryDbContext("DBGetDate");
            var repository = new TodoRepository(context);

            var todo1 = new Todo { Title = "Todo 1", Description = "Description 1", ExpiryDate = DateTime.Now.AddDays(1), PercentComplete = 50, IsDone = false };
            var todo2 = new Todo { Title = "Todo 2", Description = "Description 2", ExpiryDate = DateTime.Now.AddDays(5), PercentComplete = 100, IsDone = true };

            await repository.AddAsync(todo1);
            await repository.AddAsync(todo2);

            var dateFrom = DateTime.Now.AddDays(0);
            var dateTo = DateTime.Now.AddDays(3);

            // Act
            var todos = await repository.GetByDatesAsync(dateFrom, dateTo);

            // Assert
            Assert.Single(todos); // Should return only one todo within the given date range
            Assert.Equal("Todo 1", todos[0].Title);
        }


        [Fact]
        public async Task AddAsync_ShouldAddTodoToDatabase()
        {
            // Arrange
            var context = CreateInMemoryDbContext("DBAdd");
            var repository = new TodoRepository(context);

            var newTodo = new Todo
            {
                Title = "Test Todo",
                Description = "Test Description",
                ExpiryDate = DateTime.Now.AddDays(1),
                PercentComplete = 0,
                IsDone = false
            };

            // Act
            var response = await repository.AddAsync(newTodo);

            // Assert
            var todo = await repository.GetByIdAsync(response);
            Assert.NotNull(todo);
            Assert.Equal("Test Todo", todo.Title);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExistingTodo()
        {
            // Arrange
            var context = CreateInMemoryDbContext("DBUpdate");
            var repository = new TodoRepository(context);

            var todo = new Todo { Title = "Old Todo", Description = "Old Description", ExpiryDate = DateTime.Now, PercentComplete = 0, IsDone = false };
            var id = await repository.AddAsync(todo);
            context.Entry(todo).State = EntityState.Detached;
            // Pobierz obiekt, a następnie odłącz od kontekstu, by uniknąć konfliktu z śledzeniem
            var existingTodo = await repository.GetByIdAsync(id);
            //context.Entry(existingTodo).State = EntityState.Detached;

            // Twórz zmienioną instancję
            existingTodo.Title = "Updated Todo";
            existingTodo.Description = "Updated Description";
            existingTodo.PercentComplete = 50;
            existingTodo.IsDone = true;

            // Act
            var updateResult = await repository.UpdateAsync(existingTodo);
            var fetchedTodo = await repository.GetByIdAsync(id);

            // Assert
            Assert.True(updateResult);
            Assert.Equal("Updated Todo", fetchedTodo.Title);
            Assert.Equal("Updated Description", fetchedTodo.Description);
            Assert.Equal(50, fetchedTodo.PercentComplete);
            Assert.True(fetchedTodo.IsDone);
        }



        [Fact]
        public async Task DeleteAsync_ShouldRemoveTodoFromDatabase()
        {
            // Arrange
            var context = CreateInMemoryDbContext("DBDelete");
            var repository = new TodoRepository(context);

            var todo = new Todo { Title = "Todo to be deleted", Description = "Description", ExpiryDate = DateTime.Now, PercentComplete = 0, IsDone = false };
            var id = await repository.AddAsync(todo);

            // Act
            var deleteResult = await repository.DeleteAsync(id);
            var deletedTodo = await repository.GetByIdAsync(id);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(deletedTodo);
        }

    }
}
