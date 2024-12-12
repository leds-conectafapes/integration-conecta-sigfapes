using Hangfire;
using Integrador.Authentication.Sink;
using Integrador.Authentication.Source;
using Integrador.Authentication.Transform;
using Integrador.SigFapes.Edital.Process;

namespace Integrador.Authentication.Process;

public class AuthenticationProcess
{
    private readonly IAuthenticationSource _source;
    private readonly IAuthenticationTransform _transform;
    private readonly IAuthenticationSink _sink;
    private readonly IBackgroundJobClient _backgroundJobClient;
    
    public AuthenticationProcess(
        IAuthenticationSource source,
        IAuthenticationTransform transform,
        IAuthenticationSink sink,
        IBackgroundJobClient backgroundJobClient)
    {
        _source = source;
        _transform = transform;
        _sink = sink;
        _backgroundJobClient = backgroundJobClient;
    }
    
    public async Task Execute()
    {
        try
        {
            var request = await _source.GetAuthenticationRequest();
            var response = await _transform.TransformAuthenticationAsync(request);
            await _sink.StoreAuthenticationAsync(response);
            
            _backgroundJobClient.Enqueue<EditalProcess>(x => x.Execute());
            
            Console.WriteLine($"Autenticação completa em: {DateTime.Now}");
            Console.WriteLine("Job de editais agendado para execução");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no processo de autenticação: {ex.Message}");
            throw;
        }
    }
}