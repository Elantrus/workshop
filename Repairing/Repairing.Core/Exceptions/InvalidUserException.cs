namespace Repairing.Core.Exceptions;

public class InvalidUserException : Exception
{
    public InvalidUserException() : base("Usuário inválido.")
    {
        
    }
}