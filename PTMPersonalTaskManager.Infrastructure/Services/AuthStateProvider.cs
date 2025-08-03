using Mapster;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using PTMPersonalTaskManager.Domain.Entities;
using PTMPersonalTaskManager.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PTMPersonalTaskManager.Infrastructure.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthApiServices _authApiServices;
        private readonly ProtectedLocalStorage _localStorage;

        public AuthStateProvider(AuthApiServices authApiServices, ProtectedLocalStorage localStorage)
        {
            _authApiServices = authApiServices;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
           
            try
            {
                var results = await _localStorage.GetAsync<string>("AccessToken");
                var token = results.Success ? results.Value : null;

                if (string.IsNullOrWhiteSpace(token))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var claims = new List<Claim>();
                foreach (var claim in jwt.Claims)
                {
                    if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                    {
                        claims.Add(new Claim(ClaimTypes.Name, claim.Value));
                    }
                    else if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                    {
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, claim.Value));
                    }
                    else
                    {
                        claims.Add(new Claim(claim.Type, claim.Value));
                    }
                  
            
                }
                var identity = new ClaimsIdentity(claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }

            catch
            {
                return new AuthenticationState(new ClaimsPrincipal( new ClaimsIdentity()));
            }
         
        }
        public async Task LogoutAsync()
        {
            try
            {
                var token = await _localStorage.GetAsync<string>("AccessToken");
                var results = token.Success ? token.Value : null;
                if (!string.IsNullOrEmpty(results))
                await _authApiServices.LogoutAsync(results);

                await _localStorage.DeleteAsync("AccessToken");
                await _localStorage.DeleteAsync("RefreshToken");
            }             
            catch
            {
                await _localStorage.DeleteAsync("AccessToken");
                await _localStorage.DeleteAsync("RefreshToken");
            }
            NotifyChanges();
        }
        public void NotifyChanges()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
