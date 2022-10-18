namespace Repairing.Core.Exceptions;

public class CarNotExistsException: Exception
{
    public CarNotExistsException() : base("Carro não encontrado.")
    {
        
    }
}