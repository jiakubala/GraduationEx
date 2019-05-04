using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    public class FileController : Controller
    {
        private readonly FileServerConfig _config = null;
        public FileController(FileServerConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// objectId标注上传者
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="deviceName">驱动名：表示存在哪个文件下，配置文件中进行配置</param>
        /// <returns></returns>
        [HttpPost("{objectId}")]
        public async Task<ActionResult> Upload([FromRoute]string objectId, [FromQuery]string deviceName)
        {
            PathItem pathItem = null;
            if (!string.IsNullOrEmpty(deviceName))
            {
                pathItem = _config.PathList.FirstOrDefault(x => x.Url.ToLower() == deviceName.ToLower());
            }
            if (pathItem == null)
            {
                pathItem = _config.PathList.FirstOrDefault();
            }
            if (pathItem == null)
            {
                return new JsonResult(new
                {
                    code = "1",
                    message = "无法上传"
                });
            }

            var files = Request.Form.Files;
            if (!Request.Form.Files.Any())
            {
                return new JsonResult(new
                {
                    code = "1",
                    message = "没有文件"
                });
            }
            DateTime date = DateTime.Now;
            FileInfo fi = null;
            List<FileInfo> fileinfos = new List<FileInfo>();

            // 循环添加
            foreach (var f in Request.Form.Files)
            {
                fi = new FileInfo();

                fi.Id = Guid.NewGuid().ToString();
                fi.ContentType = f.ContentType;
                fi.Ext = System.IO.Path.GetExtension(f.FileName);
                fi.RelPath = $"{date.Year.ToString()}\\{date.Month.ToString()}\\{objectId}\\{fi.Id}{fi.Ext}";
                fi.Length = f.Length;
                fi.SrcPath = f.FileName;
                fi.Path = System.IO.Path.Combine(pathItem.LocalPath, fi.RelPath);
                fi.Url = pathItem.Url + "/" + fi.RelPath.Replace("\\", "/");

                fileinfos.Add(fi);

                // 创建目录
                string d = System.IO.Path.GetDirectoryName(fi.Path);
                try
                {
                    System.IO.Directory.CreateDirectory(d);
                }
                catch (Exception e)
                {
                    Console.WriteLine("创建目录失败：{0}\r\n{1}", d, e.ToString());
                    // Logger.Warn("创建目录失败：{0}\r\n{1}", dir, e.ToString());
                    return new JsonResult(new
                    {
                        code = "1",
                        message = "无法创建目录"
                    });
                }

                // 保存文件
                using (System.IO.FileStream fs = new System.IO.FileStream(fi.Path, System.IO.FileMode.Create))
                {
                    await f.OpenReadStream().CopyToAsync(fs);
                }
            }
            return new JsonResult(fileinfos);
        }
    }
}