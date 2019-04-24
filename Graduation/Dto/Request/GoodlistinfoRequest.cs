using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Request
{
    /// <summary>
    /// 商品分类展示页面排序请求体
    /// </summary>
    public class GoodlistinfoRequest
    {
        /// <summary>
        /// 已有的商品列表
        /// </summary>
        public List<Good> Goodlist { get; set; }

        /// <summary>
        /// 是否根据销量排序
        /// </summary>
        public int? IsSales { get; set; }

        /// <summary>
        /// 是否根据价格排序
        /// </summary>
        public int? IsPrice { get; set; }

        /// <summary>
        /// 最低价格
        /// </summary>
        public decimal? Minprice { get; set; }

        /// <summary>
        /// 最高价格
        /// </summary>
        public decimal? Maxprice { get; set; }
    }
}
