using Hangfire.MemoryStorage.Dto;
using Integrador.User.Model.Dto;

namespace Integrador.User.Transform
{
    public class UserTransformer
    {
        public List<UserDTO> Transform(string jsonData)
        {
            // Aqui simulamos a transformação do JSON em uma lista de objetos de usuários
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDTO>>(jsonData);

            // Aqui pode-se aplicar regras de transformação adicionais
            return users ?? new List<UserDTO>();
        }

    }
    
}
