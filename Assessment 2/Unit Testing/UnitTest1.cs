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
            string expected = "Michael";
            List<Server> servers = new List<Server>();
            Server server1 = new Server("Paul", 2);
            Server server2 = new Server("Michael", 8);

            servers.Add(server1);
            servers.Add(server2);

            string actual = servers.Find(x => x.ID == 8).name;

            Assert.AreEqual(expected, actual);
        }
    }
}
