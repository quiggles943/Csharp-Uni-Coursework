using Menu_Program;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ServerTest()
        {
            List<Server> servers = new List<Server>();
            Server server1 = new Server("Paul", 2);
            Server server2 = new Server("Michael", 8);

            servers.Add(server1);
            servers.Add(server2);

            string search = servers.Find(x => x.ID == 8).Name;

            Assert.AreEqual("Michael", search);
        }
        [TestMethod]
        public void MenuTest()
        {
            bool correct = false;
            List<Menu> items = new List<Menu>();
            Menu item1 = new Menu("Tomato Pizza", true, 400);
            Menu item2 = new Menu("Peperoni Pizza", false, 600);
            Menu item3 = new Menu("Tomato Pasta", true, 350);

            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            int price = 0;
            foreach(var item in items)
            {
                price = price + item.Price;
            }
            if (price == 1350)
            {
                correct = true;
            }
            Assert.IsTrue(correct, "Price not correct");
        }
    }
}
