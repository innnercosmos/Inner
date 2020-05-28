using System;
using System.Collections.Generic;
using System.Text;

namespace Inner.Model.Models
{
    public class SysUser
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; } 
        /// <summary>
        /// 账户
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
