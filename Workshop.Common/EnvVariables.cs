namespace Workshop.Common;

public static class EnvVariables
{
    
    private static readonly string? _workshopDbHost = Environment.GetEnvironmentVariable("WORKSHOP_DB");
    
    public static string WorkshopDbHost
    {
        get
        {
            if (string.IsNullOrEmpty(_workshopDbHost)) throw new Exception("WORKSHOP_DB não informado.");
            
            return _workshopDbHost;
        }
    }
}