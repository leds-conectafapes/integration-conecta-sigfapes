using Integrador.Authentication.Model.Dto;

namespace Integrador.Authentication.Sink;

public class MemoryAuthenticationSink: IAuthenticationSink
{
    private static AuthResponse _storedAuth;
    private static readonly object _lock = new object();
    
    public Task StoreAuthenticationAsync(AuthResponse response)
    {
        lock (_lock)
        {
            _storedAuth = response;
        }
        return Task.CompletedTask;
    }

    public Task<AuthResponse> GetStoredAuthenticationAsync()
    {
        lock (_lock)
        {
            if (_storedAuth == null)
            {
                throw new InvalidOperationException("Nenhum token de autenticação disponível");
            }
            return Task.FromResult(_storedAuth);
        }
    }
}