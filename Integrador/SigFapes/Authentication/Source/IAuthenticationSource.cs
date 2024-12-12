using Integrador.Authentication.Model.Dto;

namespace Integrador.Authentication.Source;


public interface IAuthenticationSource
    {
        Task<AuthRequest> GetAuthenticationRequest();
    }
