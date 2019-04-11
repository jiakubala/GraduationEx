using System;
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
    }
}
