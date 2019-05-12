using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Response
{
    /// <summary>
    /// 购物车跳转页面返回体
    /// </summary>
    public class ShopflowResponse
    {
        public List<Address> Adds { get; set; }

        public List<string> Orderidlist { get; set; }
    }
}
