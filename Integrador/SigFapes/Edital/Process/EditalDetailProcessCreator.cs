using System.Text.Json;
using Hangfire;
using Integrador.SigFapes.Edital.Model;

namespace Integrador.SigFapes.Edital.Process;

public class EditalDetailProcessCreator
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ILogger<EditalDetailProcessCreator> _logger;

    public EditalDetailProcessCreator(
        IBackgroundJobClient backgroundJobClient,
        ILogger<EditalDetailProcessCreator> logger)
    {
        _backgroundJobClient = backgroundJobClient;
        _logger = logger;
    }

    public void execute(string editaisJson)
    {
        try
        {
            var response = JsonSerializer.Deserialize<EditalResponse>(editaisJson);
            
            if (response?.Data == null || !response.Data.Any())
            {
                _logger.LogInformation("Nenhum edital encontrado para criar jobs");
                return;
            }

            _logger.LogInformation($"Criando jobs para {response.Data.Count} editais");

            foreach (var edital in response.Data)
            {
                var jobId = _backgroundJobClient.Enqueue<EditalDetailProcess>(
                    processor => processor.execute(edital.EditalId));
                
                _logger.LogInformation($"Job criado para EditalId: {edital.EditalId}, JobId: {jobId}");
            }

            _logger.LogInformation("Todos os jobs foram criados");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao criar jobs: {ex.Message}");
            throw;
        }
    }
}