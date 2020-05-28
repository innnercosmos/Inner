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
    //[Authorize(Policy ="Admin")]
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
        // GET: api/Blog/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public List<SysUser> Get(int id)
        {
            ISysUserService sysuserservice = new SysUserService();

            return sysuserservice.Query(d => d.Id == id);
        }
        ISysUserService _sysUserService = new SysUserService();
        //ISysUserRepository _sysUserService = new SysUserRepository();
        // GET: api/Blog/5
        [HttpGet]
        [Route("GetByPa")]
        public int Get(int i, int j)
        {
            ISysUserService _sysUserService = new SysUserService();
            return _sysUserService.Sum(i, j);
        }

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