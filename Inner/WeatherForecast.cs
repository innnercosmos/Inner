using System;

namespace Inner
{
    /// <summary>
    /// 天气实体
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// 
        /// </summary>
        public string Summary { get; set; }
    }
}
