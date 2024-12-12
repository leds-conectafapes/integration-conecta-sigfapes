using System.Text.Json.Serialization;

namespace Integrador.SigFapes.Edital.Model;


public class EditalResponse
{
    [JsonPropertyName("data")]
    public List<EditalData> Data { get; set; }
}