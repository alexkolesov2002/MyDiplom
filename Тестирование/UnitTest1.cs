using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Учет_работы_мастерских;

namespace Тестирование
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Check_Autotise()
        {
            users user = BaseModel.BaseConnect.users.FirstOrDefault(x => x.login == "1" && x.password == "1");
            users admin = BaseModel.BaseConnect.users.FirstOrDefault(x => x.login == "Admin" && x.password == "Admin");
            Assert.AreNotEqual(user, admin);
        }
    }
}
