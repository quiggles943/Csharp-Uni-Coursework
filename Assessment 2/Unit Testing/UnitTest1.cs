using Menu_Program;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Testing
{
    [TestClass]
    public class UnitTest1
    {
        Menu item1 = new Menu("Tomato Pizza", true, 350);
        Menu item2 = new Menu("Peperoni Pizza", false, 500);
        Menu item3 = new Menu("Spaghetti Bolonaise", false, 600);
        [TestMethod]
        public void ServerTest()
        {
            string expected = "Michael";
            List<Server> servers = new List<Server>();
            Server server1 = new Server("Paul", 2);
            Server server2 = new Server("Michael", 8);

            servers.Add(server1);
            servers.Add(server2);

            string actual = servers.Find(x => x.ID == 8).Name;

            Assert.AreEqual(expected, actual,true,"Incorrect server selected");
        }

        [TestMethod]
        public void MenuTest()
        {
            int expected = 950;
            List<Menu> items = new List<Menu>();
            items.Add(item1);
            items.Add(item2);
            items.Add(item3);
            int actual = items[0].Price + items[2].Price;

            Assert.AreEqual(expected, actual, 0.001);

        }

        [TestMethod]
        public void SitinTest()
        {
            sitinOrder s = new sitinOrder();
            int expected = 1000;
            s.Dishes(item2);
            s.Dishes(item2);
            double total = s.total();
            Assert.AreEqual(expected, total, 0.001);
        }
        [TestMethod]
        public void DeliveryTest()
        {
            deliveryOrder d = new deliveryOrder();
            int expected = 950;
            d.Dishes(item1);
            d.Dishes(item3);
            double total = d.total();
            Assert.AreEqual(expected, total, 0.001);
        }
        [TestMethod]
        public void noteTest()
        {
            sitinOrder s = new sitinOrder();
            s.Dishes(item1);
            s.addNote(0, "Extra Cheese");
            s.Dishes(item2);
            Assert.AreEqual(s.readNote(1), "None");
        }
    }
}
