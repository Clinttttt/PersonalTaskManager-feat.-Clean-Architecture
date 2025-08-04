using Azure.Core;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Identity.Client;
using PTMPersonalTaskManager.Domain.DTOs.DetailsDto;
using PTMPersonalTaskManager.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Infrastructure.Services
{
  public class TaskApiServices
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localStorage;
        public TaskApiServices(HttpClient http, ProtectedLocalStorage localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }
        public async Task SetAuthHeaderAsync()
        {
            var Results = await _localStorage.GetAsync<string>("AccessToken");
            var Token = Results.Success ? Results.Value : null;
            if (!string.IsNullOrEmpty(Token))
            {
                 _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers
                       .AuthenticationHeaderValue("Bearer", Token);
            }
        }
        public async Task<Taskproperties?> CreateTaskAsync( CreateTaskDto create)
        {
          await SetAuthHeaderAsync();
            var initialize = await _http.PostAsJsonAsync("api/TaskManager/CreateData", create);
            if (!initialize.IsSuccessStatusCode)
            {
                return null;
            }
            return await initialize.Content.ReadFromJsonAsync<Taskproperties>();
        }
        public async Task<Taskproperties?> GetDataAsync(Guid Id)
        {
            await SetAuthHeaderAsync();
            var initialize = await _http.PostAsJsonAsync($"api/TaskManager/GetData/{Id}", Id);
            if (!initialize.IsSuccessStatusCode)
            {
                return null;
            }
            return await initialize.Content.ReadFromJsonAsync<Taskproperties>();
        }
        public async Task<IEnumerable<DetailsDto>?> AllTaskAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<IEnumerable<DetailsDto>>("api/TaskManager/ListTask");
        }
        public async Task<Taskproperties?> DeleteTaskAsync( Guid Id)
        {
            await SetAuthHeaderAsync();
            var initialize = await _http.PostAsJsonAsync($"api/TaskManager/DeleteData{Id}​", Id);
            if (!initialize.IsSuccessStatusCode)
            {
                return null;
            }
            return await initialize.Content.ReadFromJsonAsync<Taskproperties>();
            
        }
        public async Task<Taskproperties?> UpdateTaskAsync(UpdateTaskDto update)
        {
            await SetAuthHeaderAsync();
            var initialize = await _http.PostAsJsonAsync("api/TaskManager/UpdateData", update);
            if (!initialize.IsSuccessStatusCode)
            {
                return null;
            }
            return await initialize.Content.ReadFromJsonAsync<Taskproperties>();
        
        }
    }
}
