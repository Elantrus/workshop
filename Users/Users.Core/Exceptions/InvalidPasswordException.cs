namespace Users.Core.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base($"Senha inválida.")
    {
        
    }
}