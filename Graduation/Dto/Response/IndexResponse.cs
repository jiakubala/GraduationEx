using Graduation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Dto.Response
{
    public class IndexResponse
    {
        public List<Good> GoodList { get; set; }

        public List<string> TypeList { get; set; }

        public string Typename { get; set; }
    }
}
