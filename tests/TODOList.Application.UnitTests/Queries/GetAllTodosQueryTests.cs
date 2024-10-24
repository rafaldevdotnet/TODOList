using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOList.Application.Interfaces;
using TODOList.Application.TODO.Queries;
using TODOList.Application.UnitTests.Mocks;
using TODOList.Domain.Entities;

namespace TODOList.Application.UnitTests.Queries
{
    public class GetAllTodosQueryTests
    {
       
        [Fact]
        public async void GetAllTodosQuery_ForTwoRecord_ReturnAllRecord()
        {
            //arrange
            var _todoRepository = MocksRepositories.GetTodoRepository();
            var handler = new GetAllTodosQueryHandler(_todoRepository.Object);

            //act

            var todos = await handler.Handle(new GetAllTodosQuery(), CancellationToken.None);

            //assert

            Assert.Equal(2, todos.Count);
        }
    }
}
