using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    /// <summary>
    /// 订单实体
    /// </summary>
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int GoodId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GoodNumber { get; set; }

        [Required]
        public int OrderState { get; set; }

        [Required]
        public string Name { get; set; }

        public string Evaluate { get; set; }

        public enum OrgStatus
        {
            /// <summary>
            /// 等待买家付款
            /// </summary>
            Unpaid = 1,

            /// <summary>
            /// 买家已付款，等待发货
            /// </summary>
            Paid = 2,

            /// <summary>
            /// 已发货，等待收货
            /// </summary>
            Shipped = 3,

            /// <summary>
            /// 已收货，完成交易
            /// </summary>
            Received = 4,

            /// <summary>
            /// 商品已下架
            /// </summary>
            Shelf = 5
        }
    }
}
