namespace Users.Core.Exceptions;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException() : base($"O email já existe.")
    {
        
    }
}