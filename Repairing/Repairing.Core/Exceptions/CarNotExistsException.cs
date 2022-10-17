namespace Repairing.Core.Exceptions;

public class CarNotExistsException: Exception
{
    public CarNotExistsException() : base("Já existe um carro com essa placa.")
    {
        
    }
}