namespace Users.Core.Exceptions;

public class RoleAlreadyExistException : Exception
{
    public RoleAlreadyExistException() : base($"A role informada já existe.")
    {
        
    }
}