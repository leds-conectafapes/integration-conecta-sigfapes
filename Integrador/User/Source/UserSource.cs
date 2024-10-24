using System.Net.Http;

namespace Integrador.User.Source
{


    public class UserSource
    {
        private readonly HttpClient _httpClient;

        public UserSource(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Retrieve()
        {
            string apiURL = "http://localhost:3000/users";
            return @"
        [
            {
                'id': '1',
                'name': 'John Doe',
                'email': 'john.doe@example.com'
            },
            {
                'id': '2',
                'name': 'Jane Smith',
                'email': 'jane.smith@example.com'
            },
            {
                'id': '3',
                'name': 'Robert Brown',
                'email': 'robert.brown@example.com'
            }
        ]";

            //await _httpClient.GetStringAsync(apiUrl);
        }


    }
}
