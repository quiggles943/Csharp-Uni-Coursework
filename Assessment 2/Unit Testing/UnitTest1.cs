using Menu_Program;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ServerTest()
        {
            Server server1 = new Server("Paul", 2);
            Server server2 = new Server("Michael", 8);


        }
    }
}
