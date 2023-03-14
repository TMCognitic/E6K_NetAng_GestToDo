using E6K_NetAng_GestToDo.Models.Entities;
using E6K_NetAng_GestToDo.Models.Repositoryies;
using System.Text.Json;

namespace E6K_NetAng_GestToDo.Models.Services
{
    public class ToDoService : IToDoRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ToDoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IEnumerable<ToDo> Get()
        {
            using(HttpClient client = _httpClientFactory.CreateClient("Default")) 
            {
                HttpResponseMessage responseMessage = client.GetAsync("ToDo").Result;
                responseMessage.EnsureSuccessStatusCode();
                string json = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<ToDo[]>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new ToDo[0];
            }
        }

        public ToDo? Get(int id)
        {
            using (HttpClient client = _httpClientFactory.CreateClient("Default"))
            {
                HttpResponseMessage responseMessage = client.GetAsync($"ToDo/{id}").Result;
                responseMessage.EnsureSuccessStatusCode();
                string json = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<ToDo>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public ToDo? Insert(ToDo entity)
        {
            using (HttpClient client = _httpClientFactory.CreateClient("Default"))
            {
                HttpContent content = JsonContent.Create(new { entity.Title });
                HttpResponseMessage responseMessage = client.PostAsync($"ToDo", content).Result;
                responseMessage.EnsureSuccessStatusCode();
                string json = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<ToDo>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public bool Update(ToDo entity)
        {
            using (HttpClient client = _httpClientFactory.CreateClient("Default"))
            {
                HttpContent content = JsonContent.Create(entity);
                HttpResponseMessage responseMessage = client.PutAsync($"ToDo", content).Result;
                return responseMessage.IsSuccessStatusCode;
            }
        }
        public bool Delete(int id)
        {
            using (HttpClient client = _httpClientFactory.CreateClient("Default"))
            {
                HttpResponseMessage responseMessage = client.DeleteAsync($"ToDo/{id}").Result;
                return responseMessage.IsSuccessStatusCode;
            }
        }
    }
}
