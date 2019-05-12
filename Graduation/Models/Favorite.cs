using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    /// <summary>
    /// 用户与商品收藏关系实体
    /// </summary>
    public class Favorite
    {
        [Key]
        [Required]
        public int KeyId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int GoodId { get; set; }
    }
}
