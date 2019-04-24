using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Request
{
    /// <summary>
    /// 商品分类分支请求体
    /// </summary>
    public class GoodlistRequest
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Typename { get; set; }

        /// <summary>
        /// 搜索名称
        /// </summary>
        public string Keyword { get; set; }


    }
}
