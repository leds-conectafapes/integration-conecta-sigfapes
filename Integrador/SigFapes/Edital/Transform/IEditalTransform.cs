namespace Integrador.SigFapes.Edital.Process;

public interface IEditalTransform
{
    Task<string> GetEditaisAsync(string token);
}