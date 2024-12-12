using Integrador.Authentication.Model.Dto;

namespace Integrador.Authentication.Transform;


public interface IAuthenticationTransform
{ 
    Task<AuthResponse> TransformAuthenticationAsync(AuthRequest request);
}
