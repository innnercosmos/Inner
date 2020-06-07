using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inner.IServices;
using Inner.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inner.Controllers
{
    /// <summary>
    /// 天气demo
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISysUserService advertisementServices;
        /// <summary>
        /// WeatherForecastController接口
        /// </summary>
        /// <returns></returns>

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISysUserService advertisementServices)
        {
            _logger = logger;
            this.advertisementServices = advertisementServices;
        }
        /// <summary>
        /// WeatherForecast_get接口
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[Authorize]//(Roles="Admin")
        ////[ApiExplorerSettings(IgnoreApi = true)] //如果不想显示某些接口，直接在controller 上，或者action 上，增加特性, 或者直接对这个方法 private，也可以直接使用obsolete属性
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        /// <summary>
        /// 获取接口数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string[] Get1()
        {
            var ads = advertisementServices.Query();
            return Summaries;
        }
    }
}
