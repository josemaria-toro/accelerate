using System;

namespace Zetatech.Accelerate.Exceptions;

public class MessagingException : Exception
{
    public MessagingException() : base()
    {
    }
    public MessagingException(String message) : base(message)
    {
    }
    public MessagingException(String message, Exception innerException) : base(message, innerException)
    {
    }
}
