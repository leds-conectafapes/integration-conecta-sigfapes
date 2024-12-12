using System.Text.Json;
using Integrador.SigFapes.Edital.Model;
using Integrador.SigFapes.Edital.Process;

namespace Integrador.SigFapes.Edital.Sink;

public class EditalSink: IEditalSink
{
    private readonly EditalDetailProcessCreator _jobCreator;
    private readonly ILogger<EditalSink> _logger;
    
    public EditalSink(
        EditalDetailProcessCreator jobCreator,
        ILogger<EditalSink> logger)
    {
        _jobCreator = jobCreator;
        _logger = logger;
    }
    public async Task ProcessEditaisAsync(string editaisJson)
    {
        try
        {
            _logger.LogInformation("Iniciando criação de jobs para editais");
            _jobCreator.execute(editaisJson);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            throw;
        }
    }
}