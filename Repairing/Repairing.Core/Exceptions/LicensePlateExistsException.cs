namespace Repairing.Core.Exceptions;

public class LicensePlateExistsException: Exception
{
    public LicensePlateExistsException() : base("Já existe um carro com essa placa.")
    {
        
    }
}