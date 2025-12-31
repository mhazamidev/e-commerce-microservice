namespace ProductService.Applilcation.Core.Exceptions;

public class ApplicationDataException : Exception
{
    public ApplicationDataException(string message) : base(message) { }
    public ApplicationDataException(string message, Exception? innerException = null) : base(message, innerException) { }
}
