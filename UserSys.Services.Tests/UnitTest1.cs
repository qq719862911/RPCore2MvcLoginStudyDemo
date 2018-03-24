using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using UserSys.Commons;
using UserSys.Services.Configs;

namespace UserSys.Services.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var t1 = typeof(UserConfig);
            var types = t1.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            System.Console.WriteLine(types.Count());

        }
        [TestMethod]
        public void TestMethod2()
        {
           var pwd = MD5Helper.Md5("qqqqq");
            File.WriteAllText(@"E:\1234555.txt", pwd);
            System.Console.WriteLine(pwd);

        }

    }
}
