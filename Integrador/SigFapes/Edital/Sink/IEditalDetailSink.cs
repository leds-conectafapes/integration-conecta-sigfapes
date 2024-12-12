namespace Integrador.SigFapes.Edital.Sink;

public interface IEditalDetailSink
{
    Task ProcessEditaisAsync(string editalJson,string editalId);
}