using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Integrador.SigFapes.Edital.Process;

public class HttpEditalTransform: IEditalTransform
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private const string FUNCAO = "editais";

    public HttpEditalTransform(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("EditalClient");
        _configuration = configuration;
    }
    
    public async Task<string> GetEditaisAsync(string token)
    {
        try
        {
            var baseUrl = _configuration["Consulta:BaseUrl"];
            var fullUrl = $"{baseUrl}/{{editais}}";

            // Configura os headers
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Cria o objeto para envio
            var requestData = new
            {
                token = token,
                funcao = FUNCAO
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