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
    public class GetTodoByDatesQueryTests
    {
        private readonly Mock<ITodoRepository> _todoRepository;

        public GetTodoByDatesQueryTests()
        {
            _todoRepository = MocksRepositories.GetTodoRepository();
        }

        [Fact]
        public async void GetTodoByDatesQuery_SearchExisting_ReturnOneRecord()
        {
            //arrange
            var handler = new GetTodoByDatesQueryHandler(_todoRepository.Object);

            //act

            var todos = await handler.Handle(new GetTodoByDatesQuery(DateTime.Today.AddDays(1), DateTime.Today.AddDays(1)), CancellationToken.None);

            //assert

            Assert.Single(todos);
        }

        [Fact]
        public async void GetTodoByDatesQuery_SearchForNonExisting_ReturnEmpty()
        {
            //arrange
            var handler = new GetTodoByDatesQueryHandler(_todoRepository.Object);

            //act

            var todos = await handler.Handle(new GetTodoByDatesQuery(DateTime.Today, DateTime.Today), CancellationToken.None);

            //assert

            Assert.Empty(todos);
        }
    }
}
