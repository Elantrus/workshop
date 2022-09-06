namespace Users.Core.Exceptions;

public class PasswordIsTooWeakException : Exception
{
    public PasswordIsTooWeakException(string weakMessage) : base($"A senha informada é muito fraca. Deveria conter pelo menos {weakMessage}")
    {
        
    }
}