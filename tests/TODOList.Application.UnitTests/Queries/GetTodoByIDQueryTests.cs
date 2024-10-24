using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Application.TODO.Queries;
using TODOList.Application.UnitTests.Mocks;

namespace TODOList.Application.UnitTests.Queries
{
    public class GetTodoByIDQueryTests
    {
        private readonly Mock<ITodoRepository> _todoRepository;

        public GetTodoByIDQueryTests()
        {
            _todoRepository = MocksRepositories.GetTodoRepository();
        }

        [Fact]
        public async void GetAllTodosByIDQuery_SearchExisting_ReturnOneRecord()
        {
            //arrange
            var handler = new GetTodoByIDQueryHandler(_todoRepository.Object);

            //act

            var todo = await handler.Handle(new GetTodoByIDQuery(2), CancellationToken.None);

            //assert

            Assert.Equal("Test 2", todo.Title);
        }

        [Fact]
        public async void GetAllTodosByIDQuery_SearchForNonExisting_ReturnNull()
        {
            //arrange
            var handler = new GetTodoByIDQueryHandler(_todoRepository.Object);

            //act

            var todo = await handler.Handle(new GetTodoByIDQuery(100), CancellationToken.None);

            //assert

            Assert.Null(todo);
        }
    }
}
