using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using Integrador.Authentication.Model.Dto;

namespace Integrador.Authentication.Transform;

public class HttpAuthenticationTransform: IAuthenticationTransform
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpAuthenticationTransform(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("AuthClient");
        _configuration = configuration;
    }
    public async Task<AuthResponse> TransformAuthenticationAsync(AuthRequest request)
    {
         try
            {
                var baseUrl = _configuration["Auth:BaseUrl"];

                // Adiciona {autenticacao} na URL
                var fullUrl = $"{baseUrl}/{{autenticacao}}";

                // Configura os headers
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Cria o objeto para envio no formato exato
                var authData = new
                {
                    username = request.Username,
                    password = request.Password
                };

                // Serializa para JSON
                var jsonContent = JsonSerializer.Serialize(authData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(fullUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                response.EnsureSuccessStatusCode();
                var rawResponse = JsonSerializer.Deserialize<AuthResponseRaw>(responseContent);
                var authResponse = new AuthResponse 
                { 
                    Token = rawResponse.token,
                    CreatedAt = DateTime.UtcNow
                };


                return authResponse;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("-------- Detalhes do Erro --------");
                Console.WriteLine($"Status Code: {ex.StatusCode}");
                Console.WriteLine($"Mensagem: {ex.Message}");
                Console.WriteLine("--------------------------------");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                Console.WriteLine("------------------------------------------");
                throw;
            }
        }
    }
