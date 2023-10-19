using Contracts;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace SyncVSAsync.Models
{
    public class PersonClient
    {
        private readonly string apiUrl = "http://localhost:5279/api/people";

        public List<Person> GetPeople()
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string jsonString = webClient.DownloadString(apiUrl);
            List<Person> resultList = JsonConvert.DeserializeObject<List<Person>>(jsonString);

            return resultList;
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            HttpClient httpClient = new HttpClient();

            string jsonString = await httpClient.GetStringAsync(apiUrl);
            List<Person> resultList = JsonConvert.DeserializeObject<List<Person>>(jsonString);

            return resultList;
        }
    }
}