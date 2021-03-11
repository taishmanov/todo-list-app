using System;
using System.Collections.Generic;
using System.Text;

namespace Company1.ToDoApp.Application.Common.Exceptions
{
    public class AppException : Exception
    // TODO: Separate Exceptions or extend this exception
    // move some to Domain layer
    {
        public AppException(string message)
            : base(message)
        {

        }
    }
}
