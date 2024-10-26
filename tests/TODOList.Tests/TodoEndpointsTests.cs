using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TODOList.Domain.Entities;
using TODOList.Application.TODO.DTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TODOList.Infrastructure;

namespace TODOList.Tests
{
    public class TodoEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TodoEndpointsTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Delete the existing `DbContextOptions` registration
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<TodoContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    // Add `DbContextOptions` with an in-memory database for testing
                    services.AddDbContext<TodoContext>(options =>
                        options.UseInMemoryDatabase("TestDb"));

                    var serviceProvider = services.BuildServiceProvider();

                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<TodoContext>();

                        // Ensure the database is created.
                        db.Database.EnsureCreated();
                        db.Todos.AddRange(
                            new Todo { Title = "Task 1", Description = "Test Task 1", ExpiryDate = DateTime.Now.AddDays(1), PercentComplete = 0 },
                            new Todo { Title = "Task 2", Description = "Test Task 2", ExpiryDate = DateTime.Now.AddDays(2), PercentComplete = 50 },
                            new Todo { Title = "Task 3", Description = "Test Task 3", ExpiryDate = DateTime.Now.AddDays(3), PercentComplete = 75 },
                            new Todo { Title = "Task 4", Description = "Test Task 4", ExpiryDate = DateTime.Now.AddDays(4), PercentComplete = 100 }
                        );

                        db.SaveChanges(); 
                    }
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetAllTodos_ShouldReturnOkAndTodos()
        {
            // Act
            var response = await _client.GetAsync("/todos");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var todos = await response.Content.ReadFromJsonAsync<List<Todo>>();
            Assert.NotNull(todos);
            Assert.True(todos.Count > 0);
        }

        [Fact]
        public async Task GetTodoById_ShouldReturnOkAndTodo_WhenTodoExists()
        {
            // Arrange
            int id = 1;

            // Act
            var response = await _client.GetAsync($"/todo/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var todo = await response.Content.ReadFromJsonAsync<Todo>();
            Assert.NotNull(todo);
            Assert.Equal(id, todo.Id);
        }

        [Fact]
        public async Task CreateTodo_ShouldReturnCreated_WhenValidData()
        {
            // Arrange
            var newTodo = new CreateTodoDto
            {
                Title = "Test Todo",
                Description = "This is a test",
                ExpiryDate = DateTime.Now.AddDays(1)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/todo", newTodo);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var createdTodo = await response.Content.ReadFromJsonAsync<CreateTodoDto>();
            Assert.NotNull(createdTodo);
            Assert.Equal(newTodo.Title, createdTodo.Title);
        }

        [Fact]
        public async Task UpdateTodo_ShouldReturnOk_WhenTodoExistsAndUpdated()
        {
            // Arrange
            var updateTodo = new Todo { Id = 1, Title = "Updated Todo 1", Description = "Updated Description", ExpiryDate = DateTime.Now.AddDays(1), PercentComplete = 50, IsDone = false };

            // Act
            var response = await _client.PutAsJsonAsync("/todo", updateTodo);
            var checkT = await _client.GetAsync($"/todo/{updateTodo.Id}");
            var todo = await checkT.Content.ReadFromJsonAsync<Todo>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(updateTodo.Title, todo.Title);
            Assert.Equal(updateTodo.Description, todo.Description);
            Assert.Equal(updateTodo.ExpiryDate, todo.ExpiryDate);
            Assert.Equal(updateTodo.PercentComplete, todo.PercentComplete);
            Assert.Equal(updateTodo.IsDone, todo.IsDone);
        }
    }
}
