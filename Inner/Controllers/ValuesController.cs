using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Inner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        public ValuesController(IHttpContextAccessor httpContext)
        {
            _accessor = httpContext;
        }
       
        // GET api/values/5
       [HttpGet]//("(jwtStr)")
        public ActionResult<IEnumerable<string>> Get(string jwtStr)
        {
            //获取token内容的方法
            //1
            var JwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = JwtHandler.ReadJwtToken(jwtStr);
            // 2
            var sub = User.FindFirst(d => d.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            // 3
            var name = _accessor.HttpContext.User.Identity.Name;
            var claims = _accessor.HttpContext.User.Claims;
            var claimTypeVal = (from item in claims
                                where item.Type == JwtRegisteredClaimNames.Email
                                select item.Value).ToList();
            return new string[] { JsonConvert.SerializeObject(jwtToken), sub, name, JsonConvert.SerializeObject(claimTypeVal) };
        }
    }
}