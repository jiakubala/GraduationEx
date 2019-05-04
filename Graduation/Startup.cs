using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using log4net;
using log4net.Config;
using log4net.Repository;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Graduation.Models;
using Microsoft.Extensions.FileProviders;

namespace Graduation
{
    public class Startup
    {
        public static ILoggerRepository Repository;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            //log4net配置
            Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Repository, new System.IO.FileInfo("log4net.config"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region 文件上传
            var fs = configuration.GetSection("FileServer");
            FileServerConfig config = fs.Get<FileServerConfig>();
            services.AddSingleton(config);
            #endregion

            services.AddDbContext<UnifiedDbContext>(options =>
            {
                options.UseMySql(configuration["Data:DefaultConnection:ConnectionString"]);
            });
            services.AddSession();
            services.AddUserDefined();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            #region 文件上传
            FileServerConfig config = app.ApplicationServices.GetService<FileServerConfig>();
            if (config != null && config.PathList != null)
            {
                List<PathItem> pathList = new List<PathItem>();
                foreach (PathItem pi in config.PathList)
                {
                    if (string.IsNullOrEmpty(pi.LocalPath))
                        continue;

                    try
                    {
                        if (!System.IO.Directory.Exists(pi.LocalPath))
                        {
                            System.IO.Directory.CreateDirectory(pi.LocalPath);
                        }
                        pathList.Add(pi);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("文件夹创建失败：\r\n{0}", e.ToString());
                    }
                }

                foreach (PathItem pi in pathList)
                {
                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(pi.LocalPath),
                        RequestPath = pi.Url
                    });
                    Console.WriteLine("路径映射：{0}-->{1}", pi.LocalPath, pi.Url);
                }
            }
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Loginin}/{id?}");
            });
        }
    }
}
