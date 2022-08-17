namespace Users.Core.Exceptions;

public class RoleNameTooShortException : Exception
{
    public RoleNameTooShortException() : base($"O nome do cargo deve ser maior.")
    {
        
    }
}