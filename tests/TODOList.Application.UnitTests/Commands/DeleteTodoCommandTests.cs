using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Application.TODO.Commands;
using TODOList.Application.UnitTests.Mocks;
using TODOList.Domain.Entities;

namespace TODOList.Application.UnitTests.Commands
{
    public class DeleteTodoCommandTests
    {
        private readonly Mock<ITodoRepository> _todoRepository;
        public DeleteTodoCommandTests()
        {
            _todoRepository = MocksRepositories.GetTodoRepository();
        }

        [Fact]
        public async void DeleteTodoCommand_DeleteExistingRecord_ReturnTrue()
        {
            //arrange
            var handler = new DeleteTodoCommandHandler(_todoRepository.Object);

            //act

            var response = await handler.Handle(new DeleteTodoCommand(2), CancellationToken.None);

            //assert

            Assert.True(response);
        }


        [Fact]
        public async void DeleteTodoCommand_DeletingNonExistentRecord_ReturnFalse()
        {
            //arrange
            var handler = new DeleteTodoCommandHandler(_todoRepository.Object);

            //act

            var response = await handler.Handle(new DeleteTodoCommand(100), CancellationToken.None);

            //assert

            Assert.False(response);
        }

    }
}
