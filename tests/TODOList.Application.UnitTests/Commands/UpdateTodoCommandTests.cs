using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Application.TODO.Commands;
using TODOList.Application.TODO.DTOs;
using TODOList.Application.UnitTests.Mocks;
using TODOList.Domain.Entities;

namespace TODOList.Application.UnitTests.Commands
{
    public class UpdateTodoCommandTests
    {
        private readonly Mock<ITodoRepository> _todoRepository;

        public UpdateTodoCommandTests()
        {
            _todoRepository = MocksRepositories.GetTodoRepository();
        }

        [Fact]
        public async void UpdateTodoCommand_UpdateRecord_ReturnTrue()
        {
            //arrange
            var handler = new UpdateTodoHandlerCommand(_todoRepository.Object);
            var todo = await _todoRepository.Object.GetByIdAsync(1);
            var updateTodo = new Todo()
            {
                Id = 5,
                Title = "Test 3",
                Description = "Test 3 Description",
                ExpiryDate = DateTime.Now.AddDays(3)
            };

            //act

            var response = await handler.Handle(new UpdateTodoCommand(todo, updateTodo), CancellationToken.None);

            //assert

            Assert.True(response);
        }

        [Fact]
        public async void UpdateTodoCommand_UpdatingNonExisttentRecord_ReturnFalse()
        {
            //arrange
            var handler = new UpdateTodoHandlerCommand(_todoRepository.Object);
            var todo = new Todo()
            {
                Id = 100,
                Title = "Test 100",
                Description = "Test 100 Description",
                ExpiryDate = DateTime.Now.AddDays(100)
            };
            var updateTodo = new Todo()
            {
                Id = todo.Id,
                Title = "Test 200",
                Description = "Test 200 Description",
                ExpiryDate = DateTime.Now.AddDays(200)
            };

            //act

            var response = await handler.Handle(new UpdateTodoCommand(todo, updateTodo), CancellationToken.None);

            //assert

            Assert.False(response);
        }
    }
}
