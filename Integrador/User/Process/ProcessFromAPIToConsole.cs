using Integrador.User.Sink;
using Integrador.User.Source;
using Integrador.User.Transform;

namespace Integrador.User.Process
{
    public class ProcessFromAPIToConsole
    {
        private readonly UserSource _apiSource;
        private readonly UserTransformer _dataTransformer;
        private readonly ConsoleSink _consoleSink;

        public ProcessFromAPIToConsole(UserSource apiSource, UserTransformer dataTransformer, ConsoleSink consoleSink)
        {
            _apiSource = apiSource;
            _dataTransformer = dataTransformer;
            _consoleSink = consoleSink;
        }

        public async Task Execute()
        {
            // Source: Busca os dados da API
            string apiData = await _apiSource.Retrieve();

            // Transform: Transforma os dados recebidos
            var transformedData = _dataTransformer.Transform(apiData);

            // Sink: Imprime os dados transformados no console
            _consoleSink.PrintToConsole(transformedData);
        }
    }

}
