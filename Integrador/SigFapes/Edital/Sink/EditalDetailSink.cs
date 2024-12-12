using System.Text;
using System.Text.Json;
using Integrador.SigFapes.Edital.Model;
using Integrador.SigFapes.Edital.Process;
using Integrador.Util;

namespace Integrador.SigFapes.Edital.Sink;

public class EditalDetailSink: IEditalDetailSink
{
    private readonly ILogger<EditalDetailSink> _logger;
    private readonly IConfiguration _configuration;
    private readonly string _baseDirectory;
    
    public EditalDetailSink(
        ILogger<EditalDetailSink> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        
        // Pega o diretório base da configuração ou usa um padrão
        _baseDirectory = _configuration["EditalProcessor:OutputDirectory"] ?? "Editais";
        if (!Directory.Exists(_baseDirectory))
        {
            Directory.CreateDirectory(_baseDirectory);
        }
    }
    public async Task ProcessEditaisAsync(string editalJson, string editalId)
    {
        try
        {
            var cleanedJson = JsonCleaner.CleanJson(editalJson);
            
            var fileName = $"edital_{editalId}_{DateTime.Now:yyyyMMdd_HHmmss}.json";
            var filePath = Path.Combine(_baseDirectory, fileName);

            // Salva o arquivo
            await File.WriteAllTextAsync(filePath, cleanedJson, Encoding.UTF8);
                 _logger.LogInformation($"Processamento concluído para EditalId: {editalId}");
            
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro: {ex.Message}");
            throw;
        }
    }
}