using Graduation.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Managers
{
    /// <summary>
    /// 首页逻辑层
    /// </summary>
    public class IndexManager
    {
        protected IIndexStore _indexStore { get; }
        public IndexManager(IIndexStore indexStore)
        {
            _indexStore = indexStore;
        }
    }
}
