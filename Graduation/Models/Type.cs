using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    /// <summary>
    /// 商品分类
    /// </summary>
    public class Type
    {
        public int Id { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
