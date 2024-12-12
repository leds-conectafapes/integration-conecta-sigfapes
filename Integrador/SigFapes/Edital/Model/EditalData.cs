using System.Text.Json.Serialization;

namespace Integrador.SigFapes.Edital.Model;

public class EditalData
{
    [JsonPropertyName("edital_id")]
    public string EditalId { get; set; }
}