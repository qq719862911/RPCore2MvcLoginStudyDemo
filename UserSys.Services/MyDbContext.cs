using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace UserSys.Services
{
    public class MyDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        /// <summary>
        /// 配置相关
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder =
          new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())//SetBasePath设置配置文件所在路径
          .AddJsonFile("appsettings.json");
            var configRoot = builder.Build();
            var connString =
          configRoot.GetSection("db").GetSection("ConnectionString").Value;//获取连接字符串
            optionsBuilder.UseSqlServer(connString);//使用这个连接字符串去连接SqlServer
        }
        /// <summary>
        /// 模型映射配置
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //方法1.手动添加每一个config
            //modelBuilder.ApplyConfiguration(new UserConfig());

            //方法2.调用如鹏封装的nuget，放入程序集，直接注册里面的config
            Assembly asmServices = Assembly.Load(new AssemblyName("UserSys.Services"));//加载程序集
            modelBuilder.ApplyConfigurationsFromAssembly(asmServices);
        }

    }
}
