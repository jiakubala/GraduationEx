﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    /// <summary>
    /// 商品实体
    /// </summary>
    public class Good
    {
        [Key]
        public int GoodId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int Sold { get; set; }

        public string Details { get; set; }

        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 收藏数量
        /// </summary>
        public int Facount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime { get; set; }
    }
}
