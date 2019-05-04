using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduation.Models
{
    /// <summary>
    /// 文件上传解析路径
    /// </summary>
    public class FileServerConfig
    {
        public List<PathItem> PathList { get; set; }
    }

    public class PathItem
    {
        public string LocalPath { get; set; }
        public string Url { get; set; }
    }
}
