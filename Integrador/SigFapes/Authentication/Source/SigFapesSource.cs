using Integrador.Authentication.Model.Dto;

namespace Integrador.Authentication.Source;

public class SigFapesSource: IAuthenticationSource
{
    private readonly IConfiguration _configuration;

    public SigFapesSource(IConfiguration configuration)
    {
        _configuration = configuration;
       
    }
    
    public Task<AuthRequest> GetAuthenticationRequest()
    {
        var request = new AuthRequest
        {
            Username = _configuration["Auth:Username"],
            Password = _configuration["Auth:Password"]
        };

        return Task.FromResult(request);
    }
    
    
}