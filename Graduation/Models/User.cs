using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string TrueName { get; set; }

        public string Email { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Sex { get; set; }
    }
}
