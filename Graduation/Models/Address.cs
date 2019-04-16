using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    public class Address
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [Required]
        public int KeyId { get; set; }

        /// <summary>
        /// 收货人名字
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 收货人所在地
        /// </summary>
        [Required]
        public string Local { get; set; }

        /// <summary>
        /// 收货人详细地址
        /// </summary>
        [Required]
        public string Addres { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public int ZipCode { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Required]
        public int Phone { get; set; }

        /// <summary>
        /// 收货人ID
        /// </summary>
        [Required]
        public int UserId { get; set; }
    }
}
