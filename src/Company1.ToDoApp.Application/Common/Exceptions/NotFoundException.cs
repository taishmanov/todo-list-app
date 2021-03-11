using System;
using System.Collections.Generic;
using System.Text;

namespace Company1.ToDoApp.Application.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message)
            : base(message)
        {

        }
    }
}
