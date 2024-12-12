namespace Integrador.SigFapes.Edital.Sink;

public interface IEditalSource
{
    Task<string> GetTokenAsync();
}