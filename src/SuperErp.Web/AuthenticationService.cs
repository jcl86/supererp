using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SuperErp.Management.Model;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SuperErp.Web
{
    public class AuthenticationService
    {
        private readonly HttpClient client;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            this.client = client;
            this.authStateProvider = authStateProvider;
            this.localStorage = localStorage;
        }

        public async Task<AuthenticationSuccessResponse> Login(LoginRequest request)
        {
            var content = JsonSerializer.Serialize(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsJsonAsync("api/accounts/login", request);
            var result = await response.Content.ReadFromJsonAsync<AuthenticationSuccessResponse>();

            await localStorage.SetItemAsync("authToken", result.Token);
            ((JwtTokenAuthenticationStateProvider)authStateProvider).NotifyUserAuthentication(request.Email);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

            return result;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((JwtTokenAuthenticationStateProvider)authStateProvider).NotifyUserLogout();
            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
