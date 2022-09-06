namespace Users.Core.Exceptions;

public class UserHasNoRoleException : Exception
{
    public UserHasNoRoleException() : base($"Usuário sem cargo definido.")
    {
        
    }
}