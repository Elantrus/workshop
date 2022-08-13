namespace Users.Domain.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException() : base($"E-mail inválido.")
    {
        
    }
}