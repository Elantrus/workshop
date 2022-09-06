namespace Users.Core.Exceptions;

public class PasswordTooShortException : Exception
{
    public PasswordTooShortException() : base ($"A senha deve ter pelo menso 8 caractéres.")
    {
        
    }
}