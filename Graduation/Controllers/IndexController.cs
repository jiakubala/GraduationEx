using Graduation.Managers;
using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Controllers
{
    /// <summary>
    /// 首页服务层
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : Controller
    {
        private readonly IndexManager _indexManager;
        private ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(IndexController));

        public IndexController(IndexManager indexManager)
        {
            _indexManager = indexManager;
        }
    }
}
