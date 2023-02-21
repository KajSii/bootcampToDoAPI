using System;

namespace Bootcamp.ToDoList.Backend.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string message)
            : base(message)
        { }
    }
}
