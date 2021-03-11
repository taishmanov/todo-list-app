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
    public class CreateTaskCommandDefaultHandler
    {
        [Fact]
        public async Task Should_CreateNewTask()
        {
            var items = new Dictionary<string, TaskToDo>();
            var collection = new InMemoryItemsCollection<TaskToDo>(items);

            var handler = new DefaultCommandHandlers(collection);
            var id = await handler.Handle(new CreateTaskCommand()
            {
                Name = "task1",
                Priority = 1,
                Statues = "NotStarted"
            }, CancellationToken.None);

            Assert.True(collection.Items.Any());
        }

        [Fact]
        public async Task ShouldNot_CreateDuplicateTask()
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
            var newTaskCmd = new CreateTaskCommand()
            {
                Name = "task1",
                Priority = 1,
                Statues = "NotStarted"
            };

            var ex = await Assert.ThrowsAsync<AppException>(async () 
                => await handler.Handle(newTaskCmd, CancellationToken.None));
            Assert.Equal("Task with the same name already exists", ex.Message);
        }

        [Fact]
        public async Task ShouldNot_CreateTask_Without_Name()
        {
            var items = new Dictionary<string, TaskToDo>();
            var collection = new InMemoryItemsCollection<TaskToDo>(items);

            var handler = new DefaultCommandHandlers(collection);
            var newTaskCmd = new CreateTaskCommand()
            {
                Name = "",
                Statues = "NotStarted"
            };

            var ex = await Assert.ThrowsAsync<AppException>(async ()
                => await handler.Handle(newTaskCmd, CancellationToken.None));
            Assert.Equal("Task name can't be empty", ex.Message);
        }

        [Fact]
        public void Should_CreateAllTasks_In_Parallel()
        {
            var items = new Dictionary<string, TaskToDo>();
            var collection = new InMemoryItemsCollection<TaskToDo>(items);

            var handler = new DefaultCommandHandlers(collection);
            var total = 100;

            var result = Parallel.For(1, total + 1, async (i, state) =>
            {
                var newTaskCmd = new CreateTaskCommand()
                {
                    Name = $"task {i}",
                    Statues = "NotStarted"
                };
                await handler.Handle(newTaskCmd, CancellationToken.None);
            });
            
            Assert.Equal(total, collection.Items.Count());
        }

        // TODO: Test Update/Delete/Create in parallel
    }
}
