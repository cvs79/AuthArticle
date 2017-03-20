using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Auth0APIArticle.Models;

namespace Auth0APIArticle.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PingAPIController : Controller
    {
        // GET api/values
        [HttpGet]
        public List<Claims>  Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;


            var claimsList = new List<Claims>();

            foreach (var cl in claimsIdentity.Claims)
            {
                claimsList.Add(new Claims
                {
                    ClaimType = cl.Type,
                    ClaimValue = cl.Value

                });
            }

            return claimsList;
        }

    }
}
