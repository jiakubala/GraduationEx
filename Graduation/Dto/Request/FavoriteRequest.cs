using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Request
{
    /// <summary>
    /// 收藏请求体
    /// </summary>
    public class FavoriteRequest
    {
        public int? UserId { get; set; }
        public int GoodId { get; set; }
    }
}
