namespace Users.Core.Exceptions;

public class RoleNotFoundException : Exception
{
    public RoleNotFoundException() : base($"O cargo informado não existe.")
    {
        
    }
}