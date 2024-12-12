namespace Integrador.SigFapes.Edital.Process;

public interface IEditalDetailTransform
{
    Task<string> GetEditaisAsync(string token, string editalId);
}