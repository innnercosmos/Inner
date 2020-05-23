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
        public string Name
        {
            get;
            set;
        }

        public string Password { get; set; }
    }
}
