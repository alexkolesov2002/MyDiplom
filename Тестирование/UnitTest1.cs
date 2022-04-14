using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
        [TestMethod]
        public void Check_Autotise_IsNotNull()
        {
            users user = BaseModel.BaseConnect.users.FirstOrDefault(x => x.login == "1" && x.password == "1");
          
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public void Check_SelectWork()
        {
            List<workshops> w1 = BaseModel.BaseConnect.workshops.ToList();
            List<workshops> w2 = BaseModel.BaseConnect.workshops.ToList();
            Assert.AreNotEqual(w1, w2);

        }
        [TestMethod]
        public void Check_TakeEquip()
        {

            List<equipments> w1 = BaseModel.BaseConnect.equipments.ToList();
            List<equipments> w2 = BaseModel.BaseConnect.equipments.ToList();
            Assert.AreNotEqual(w1, w2);
        }
        [TestMethod]
        public void Check_JournalUseWorkShop()
        {
            List<journal_use_workshop> w1 = BaseModel.BaseConnect.journal_use_workshop.ToList();
            List<journal_use_workshop> w2 = BaseModel.BaseConnect.journal_use_workshop.ToList();
            Assert.AreNotEqual(w1, w2);
        }
        [TestMethod]
        public void Check_User ()
        {
            List <users> user = BaseModel.BaseConnect.users.ToList();
            List <users> user2 = BaseModel.BaseConnect.users.ToList();
           
            Assert.AreNotEqual(user, user2);
        }
        [TestMethod]
        public void Check_Roles()
        {
            List<roles> r1 = BaseModel.BaseConnect.roles.ToList();
            List<roles> r2 = BaseModel.BaseConnect.roles.ToList();

            Assert.AreNotEqual(r1, r2);
        }
    }
}
