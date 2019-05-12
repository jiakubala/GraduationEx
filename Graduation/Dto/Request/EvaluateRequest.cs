using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Request
{
    /// <summary>
    /// 添加评价请求体
    /// </summary>
    public class EvaluateRequest
    {
        public string Orderid { get; set; }
        public string Evaluate { get; set; }
    }
}
