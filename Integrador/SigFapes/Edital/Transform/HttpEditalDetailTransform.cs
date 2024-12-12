using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Integrador.SigFapes.Edital.Process;

public class HttpEditalDetailTransform: IEditalDetailTransform
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private const string FUNCAO = "edital_objetos_filhos";

    public HttpEditalDetailTransform(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("EditalDetailClient");
        _configuration = configuration;
    }
    
    public async Task<string> GetEditaisAsync(string token,string editalId)
    {
        try
        {
            var baseUrl = _configuration["Consulta:BaseUrl"];
            var fullUrl = $"{baseUrl}/{{edital_objetos_filhos}}";

            // Configura os headers
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Cria o objeto para envio
            var requestData = new
            {
                token = token,
                funcao = FUNCAO,
                codedt = editalId
            };
            
            // Serializa para JSON
            var jsonContent = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(fullUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status Code: {response.Headers}");

            Console.WriteLine($"Status Code: {(int)response.StatusCode} ({response.StatusCode})");
            
            response.EnsureSuccessStatusCode();
            return responseContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro na transformação Editais: {ex.Message}");
            throw;
        }
    }
}