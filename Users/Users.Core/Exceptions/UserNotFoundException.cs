namespace Users.Core.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base($"O usuário informado não existe.")
    {
        
    }
}