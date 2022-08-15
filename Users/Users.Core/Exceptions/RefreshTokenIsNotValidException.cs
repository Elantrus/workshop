namespace Users.Core.Exceptions;

public class RefreshTokenIsNotValidException : Exception
{
    public RefreshTokenIsNotValidException() : base($"Credenciais inválidas.")
    {
        
    }
}