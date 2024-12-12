using System.Text.Json;

namespace Integrador.Util;

public static class JsonCleaner
{
    public static string CleanJson(string originalJson)
    {
        try
        {
            // Deserializa o JSON original
            using var document = JsonDocument.Parse(originalJson);
            
            // Acessa apenas o array de dados
            var dataArray = document.RootElement.GetProperty("data") .EnumerateArray()
                .First();
            
            // Serializa de volta apenas o array
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(dataArray, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao limpar JSON: {ex.Message}");
            throw;
        }
    }
}