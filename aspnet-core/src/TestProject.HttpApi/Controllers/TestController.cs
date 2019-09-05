using Microsoft.AspNetCore.Mvc;
using TestProject.Models.Test;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Security.Claims;
using System.Linq;

namespace TestProject.Controllers
{
    [Route("api/test")]
    public class TestController : TestProjectController
    {

        private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;


        public TestController(ICurrentPrincipalAccessor currentPrincipalAccessor)
        {
            _currentPrincipalAccessor = currentPrincipalAccessor;
        }

        [HttpGet]
        [Route("")]
        public async Task<List<TestModel>> GetAsync()
        {
            return new List<TestModel>
            {
                new TestModel {Name = "John", BirthDate = new DateTime(1942, 11, 18)},
                new TestModel {Name = "Adams", BirthDate = new DateTime(1997, 05, 24)}
            };
        }

        [HttpGet]
        [Route("Claims")]
        public ActionResult Claims()
        {
            var userIdClaimType=AbpClaimTypes.UserId;
            return Json(new
            {
                Claims = _currentPrincipalAccessor.Principal.Claims.Select(p => new { p.Type, p.Value }),
                CurrentUser = CurrentUser.Id
            });
        }
    }
}
