﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Request
{
    /// <summary>
    /// 修改状态请求体
    /// </summary>
    public class StateRequest
    {
        public int OrderId { get; set; }

        public int OrderState { get; set; }
    }
}
