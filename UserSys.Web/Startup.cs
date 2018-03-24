using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserSys.IServices;
using UserSys.Web.Commons;

namespace UserSys.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options=>options.Filters.Add(new ModelStateValidationFilter()));//注册filter
            services.AddSession();//注册session

            //注册服务和实现类
            Assembly asmServices = Assembly.Load("UserSys.Services");
          var serviceTypes =  asmServices.GetTypes().Where(asmType => asmType.IsAbstract == false && typeof(IServiceTag).IsAssignableFrom(asmType));//接口 is assignable 子类
            foreach (var serviceType in serviceTypes)
            {
                //获取serviceType接口
              var intfTypes =  serviceType.GetInterfaces().Where(tt => typeof(IServiceTag).IsAssignableFrom(tt));
                foreach (var intfType in intfTypes)
                {
                    services.AddSingleton(intfType, serviceType);//implementation 实行，要注册的类型
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Login}/{id?}");
            });
        }
    }
}
