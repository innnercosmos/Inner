using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inner.IServices;
using Inner.Model.Models;
using Inner.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class BlogController : ControllerBase
    {
        public BlogController()
        {

        }
        [HttpGet]
        [Route("Get")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("{id}", Name = "Get")]
        public Task<List<SysUser>> Get(int id)
        {
            ISysUserService advertisementServices = new SysUserService();

            return advertisementServices.Query(d => d.Id == id);
        }
        ISysUserService _sysUserService = new SysUserService();
        //ISysUserRepository _sysUserService = new SysUserRepository();
        // GET: api/Blog/5
        //[HttpGet]
        //[Route("GetByPa")]
        //public int Get(int i, int j)
        //{
        //    ISysUserService _sysUserService = new SysUserService();
        //    return _sysUserService.Sum(i, j);
        //}

        // POST: api/Blog
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Blog/5
        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}