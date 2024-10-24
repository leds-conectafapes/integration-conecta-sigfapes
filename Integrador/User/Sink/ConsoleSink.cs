using Integrador.User.Model.Dto;

namespace Integrador.User.Sink
{
    public class ConsoleSink
    {
        public void PrintToConsole(List<UserDTO> users)
        {
            Console.WriteLine("===== Dados de Usuários Transformados =====");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Nome: {user.Name}, Email: {user.Email}");
            }
            Console.WriteLine("==========================================");
        }
    }
}
