using Moq;
using TODOList.Application.TODO.DTOs;
using TODOList.Application.Interfaces;
using TODOList.Application.TODO.Commands;
using TODOList.Application.UnitTests.Mocks;

namespace TODOList.Application.UnitTests.Commands
{
    public class CreateNewTodoCommandTests
    {        
        private readonly Mock<ITodoRepository> _todoRepository;

        public CreateNewTodoCommandTests()
        {
            
            _todoRepository = MocksRepositories.GetTodoRepository();
        }

        [Fact]
        public async void CreateNewTodoCommand_AddRecord_ReturnIdNewRecord()
        {
            //arrange
            var handler = new CreateNewTodoCommandHandler(_todoRepository.Object);
            var newTodo = new CreateTodoDto()
            {
                Title = "Test 3",
                Description = "Test 3 Description",
                ExpiryDate = DateTime.Now.AddDays(3)
            };

            //act

            var todo = await handler.Handle(new CreateNewTodoCommand(newTodo), CancellationToken.None);

            //assert

            Assert.Equal(3, todo);
        }
    }
}
