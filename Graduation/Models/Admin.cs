using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    /// <summary>
    /// 管理员实体
    /// </summary>
    public class Admin
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
