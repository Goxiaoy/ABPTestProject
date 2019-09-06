using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Security.Claims;
using System.Linq;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Users;

namespace TestProject.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {

        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;
        private readonly ICurrentUser _currentUser;

        private readonly IHttpClientFactory _httpClientFactory;

        public TestController(ICurrentPrincipalAccessor currentPrincipalAccessor, ICurrentUser currentUser, IHttpClientFactory httpClientFactory)
        {
            _currentPrincipalAccessor = currentPrincipalAccessor;
            _currentUser = currentUser;
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        [Route("Claims")]
        [Authorize]
        public ActionResult Claims()
        {
            var userIdClaimType=AbpClaimTypes.UserId;
            return Json(new
            {
                Claims = _currentPrincipalAccessor.Principal.Claims.Select(p => new { p.Type, p.Value }),
                CurrentUser = _currentUser.Id
            });
        }

        [HttpGet]
        [Route("Token")]
        public async Task<IActionResult> Token()
        {
            var client = _httpClientFactory.CreateClient("test");
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
            {
                Address = "https://localhost:44368",
                Policy = new DiscoveryPolicy()
                {
                    RequireHttps = false
                },
                
            });
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "TestProject_App",
                ClientSecret = "1q2w3e*",
                UserName = "admin",
                Password = "1q2w3E*",
                Scope = "openid profile TestProject"
            });
            return Ok(tokenResponse);
        }
    }
}
