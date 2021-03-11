using Company1.ToDoApp.Application.Tasks.Commands.Handlers;
using Company1.ToDoApp.Application.Tasks.Commands;
using Company1.ToDoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using System.Linq;
using Company1.ToDoApp.Application.Common.Exceptions;

namespace Company1.ToDoApp.Application.Tests.Tasks.Commands
{
    public class DeleteTaskCommandDefaultHandler
    {
        [Fact]
        public async Task Should_DeleteTask()
        {
            var items = new Dictionary<string, TaskToDo>()
            {
                {
                    "task1", new TaskToDo()
                    {
                        Name = "task1",
                        StatusCode = "Completed"
                    }
                }
            };
            var collection = new InMemoryItemsCollection<TaskToDo>(items);

            var handler = new DefaultCommandHandlers(collection);
            var deleted = await handler.Handle(new DeleteTaskCommand()
            {
                Id = "task1"
            }, CancellationToken.None);

            Assert.True(deleted);
            Assert.False(collection.Items.Any());
        }

        [Fact]
        public async Task ShouldNot_Delete_Incomplete_Task()
        {
            var items = new Dictionary<string, TaskToDo>()
            {
                { 
                    "task1", new TaskToDo() 
                    {
                        Name = "task1",
                        StatusCode = "NotStarted"
                    } 
                }
            };
            var collection = new InMemoryItemsCollection<TaskToDo>(items);

            var handler = new DefaultCommandHandlers(collection);
            var deleteTaskCmd = new DeleteTaskCommand()
            {
                Id = "task1"
            };

            var ex = await Assert.ThrowsAsync<AppException>(async () 
                => await handler.Handle(deleteTaskCmd, CancellationToken.None));

            Assert.Equal("Only 'Completed' task can be deleted", ex.Message);
        }
    }
}
