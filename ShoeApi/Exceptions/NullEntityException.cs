using System;

namespace WebApplication.Exceptions
{
    [Serializable]
    public class NullEntityException: Exception
    {
        public long Id { get; set; }

        public NullEntityException()
        {
        }

        public NullEntityException(string message) : base(message)
        {
            
        }

        public NullEntityException(string message, Exception ex) : base(message, ex)
        {
            
        }

        public NullEntityException(string message, long id) : this(message)
        {
            Id = id;
        }
    }
}