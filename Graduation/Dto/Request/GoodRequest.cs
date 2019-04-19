using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Request
{
    public class GoodRequest
    {
        public int OrderId { get; set; }

        public int GoodId { get; set; }

        public int UserId { get; set; }

        public int GoodNumber { get; set; }

        public int OrderState { get; set; }

        public string Name { get; set; }
    }
}
