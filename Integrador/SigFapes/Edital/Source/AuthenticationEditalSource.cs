using Integrador.Authentication.Sink;

namespace Integrador.SigFapes.Edital.Sink;

public class AuthenticationEditalSource: IEditalSource
{
    private readonly IAuthenticationSink _authSink;

    public AuthenticationEditalSource(IAuthenticationSink authSink)
    {
        _authSink = authSink;
    }

    public async Task<string> GetTokenAsync()
    {
        var authResponse = await _authSink.GetStoredAuthenticationAsync();
        return authResponse.Token;
    }
}