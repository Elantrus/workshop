namespace Users.Core.Exceptions;

public class FullNameTooShortException : Exception
{
    public FullNameTooShortException() : base($"O nome informado é muito curto.")
    {
        
    }
}