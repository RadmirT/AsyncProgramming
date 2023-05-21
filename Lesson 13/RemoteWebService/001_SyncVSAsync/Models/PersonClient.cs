using Contracts;
using Newtonsoft.Json;

namespace SyncVSAsync.Models
{
    public class PersonClient(HttpClient httpClient)
    {
        private const string ApiPath = "/api/people";

        public List<Person> GetPeople()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, ApiPath);
            using var response = httpClient.Send(request, HttpCompletionOption.ResponseContentRead);
            return ReadPeople(response);
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, ApiPath);
            using var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            return ReadPeople(response);
        }

        private static List<Person> ReadPeople(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            using var reader = new StreamReader(response.Content.ReadAsStream());
            return JsonConvert.DeserializeObject<List<Person>>(reader.ReadToEnd())
                ?? throw new JsonSerializationException("The people API returned null.");
        }
    }
}
