using Integrador.SigFapes.Edital.Sink;

namespace Integrador.SigFapes.Edital.Process;

public class EditalDetailProcess
{
    private readonly ILogger<EditalDetailProcess> _logger;
    
    private readonly IEditalSource _source;

    private readonly IEditalDetailTransform _transform;

    private readonly IEditalDetailSink _sink;

    public EditalDetailProcess(ILogger<EditalDetailProcess> logger,  IEditalSource source, IEditalDetailSink sink,IEditalDetailTransform transform)
    {
        _logger = logger;
        _source = source;
        _transform = transform;
        _sink = sink;

    }

    public async Task execute(string editalId)
    {
        try
        {
            _logger.LogInformation($"Job iniciado para EditalId: {editalId}");
            
            // Obtém o token
            var token = await _source.GetTokenAsync();

            // Obtém os editais
            var editaisJson = await _transform.GetEditaisAsync(token, editalId);

            // Processa e imprime os editais
            await _sink.ProcessEditaisAsync(editaisJson,editalId);
            
            _logger.LogInformation($"Job concluído para EditalId: {editalId}");
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro no job do EditalId {editalId}: {ex.Message}");
            throw;
        }
    }
}