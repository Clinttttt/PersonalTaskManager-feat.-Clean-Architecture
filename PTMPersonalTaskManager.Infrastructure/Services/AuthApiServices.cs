using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using PTMPersonalTaskManager.Domain.DTOs;
using PTMPersonalTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Infrastructure.Services
{
    public class AuthApiServices
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localStorage;
        public AuthApiServices (HttpClient http, ProtectedLocalStorage localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<TokenResponseDto?> LoginAsync (UserDto user)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/Login​",user);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
        }
        public async Task<User?> RegisterAsync (UserDto user)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/Register", user);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<User>();
        }
        public async Task<TokenResponseDto?> RequestTokenAsync (RefreshTokenDto request)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/Request-Token", request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
        }
        public async Task<bool> LogoutAsync (string AccessToken)
        {
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers
                .AuthenticationHeaderValue("Bearer ", AccessToken);
            var response = await _http.PostAsync("api/Auth/Logout", null);
            _http.DefaultRequestHeaders.Authorization = null;
            return response.IsSuccessStatusCode;
        }       
    }
}
