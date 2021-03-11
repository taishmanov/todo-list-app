using System;
using System.Collections.Generic;
using System.Text;

namespace Company1.ToDoApp.Domain.ReferenceData
{
    public class TaskToDoStatus
    {
        public static readonly TaskToDoStatus NotStarted = new TaskToDoStatus("NotStarted", "Not Started");
        public static readonly TaskToDoStatus InProgress = new TaskToDoStatus("InProgress", "In Progress");
        public static readonly TaskToDoStatus Completed = new TaskToDoStatus("Completed", "Completed");

        public string Code { get; private set; }
        public string Name { get; private set; }
        private TaskToDoStatus(string code, string name)
        {
            Code = code;
            Name = name;
        }

        // TODO: Equals, ToString
    }
}
