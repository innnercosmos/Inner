using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Inner.Controllers
{
    /// <summary>
    /// 登录管理【无权限】
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //readonly ISysUserInfoServices _sysUserInfoServices;

  
        public LoginController()
        {
   
        }
        [HttpGet]
        public string GetJwtStr()
        {
            //创建声明数组
            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name, "laozhang"),
            new Claim(JwtRegisteredClaimNames.Email, "1aozhang@qq. com"),
            new Claim(JwtRegisteredClaimNames.Sub, "1")
            };//主题subject,就是id uid
            //3 + 2
            SecurityToken securityToken = new JwtSecurityToken(
                issuer: "Issuer",//发行人
                audience: "Audience",//订阅人
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey
                (Encoding.ASCII.GetBytes("innerinnerinnerinner")), SecurityAlgorithms.HmacSha256),//数字加密
                expires: DateTime.Now.AddHours(1),//过期日期
                claims: claims//声明数组 
                );
                return new JwtSecurityTokenHandler(). WriteToken(securityToken);
        }




    }
}