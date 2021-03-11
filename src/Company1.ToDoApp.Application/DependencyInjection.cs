using Company1.ToDoApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

namespace Company1.ToDoApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            return services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddSingleton(new InMemoryItemsCollection<TaskToDo>(new Dictionary<string, TaskToDo>()));
        }
    }
}
