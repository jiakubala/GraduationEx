using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Request
{
    /// <summary>
    /// 修改密码请求体
    /// </summary>
    public class PasswordRequest
    {
        public int? UserId { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string Oldpassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string Newpassword { get; set; }

        /// <summary>
        /// 重复输入
        /// </summary>
        public string Upassword { get; set; }
    }
}
