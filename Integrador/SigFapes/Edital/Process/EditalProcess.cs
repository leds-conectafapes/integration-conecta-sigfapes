using Integrador.SigFapes.Edital.Sink;

namespace Integrador.SigFapes.Edital.Process;

public class EditalProcess
{
    private readonly IEditalSource _source;
    private readonly IEditalTransform _transform;
    private readonly IEditalSink _sink;
    
    public EditalProcess(IEditalSource source, IEditalTransform transform, IEditalSink sink)
    {
        _source = source;
        _transform = transform;
        _sink = sink;
    }

    public async Task Execute()
    {
        try
        {
            // Obtém o token
            var token = await _source.GetTokenAsync();

            // Obtém os editais
            var editaisJson = await _transform.GetEditaisAsync(token);

            // Processa e imprime os editais
            await _sink.ProcessEditaisAsync(editaisJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no orquestrador de editais: {ex.Message}");
            throw;
        }
    }
    



}