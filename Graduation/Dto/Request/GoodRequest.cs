using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Request
{
    /// <summary>
    /// 购物车请求体
    /// </summary>
    public class GoodRequest
    {
        public int OrderId { get; set; }

        public int GoodId { get; set; }

        public int? UserId { get; set; }

        public int GoodNumber { get; set; }

        public int OrderState { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
