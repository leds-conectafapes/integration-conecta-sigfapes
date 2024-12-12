namespace Integrador.SigFapes.Edital.Sink;

public interface IEditalSink
{
    Task ProcessEditaisAsync(string editaisJson);
}