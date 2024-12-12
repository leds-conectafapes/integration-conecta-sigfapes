using Integrador.Authentication.Model.Dto;

namespace Integrador.Authentication.Sink;

public interface IAuthenticationSink
{
    Task StoreAuthenticationAsync(AuthResponse response);
    Task<AuthResponse> GetStoredAuthenticationAsync();
}