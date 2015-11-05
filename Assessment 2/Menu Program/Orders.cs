using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using System.Collections;
namespace Menu_Program
{
    public class Order
    {
        public List<Menu> items = new List<Menu>();
        public List<Order> sitinorders = new List<Order>();
        public List<Order> deliveryorders = new List<Order>();
        string server;
        string driver;
        string name;
        int table;
        double paid;
        string[] orders = new string[10];
        public Order()
        {

        }
        public void Clear()
        {
            items.Clear();
        }
        public void Details(string s, int t, double p )
        {
            int i;
            i = sitinorders.Count;
            sitinorders.Add( new Order { server = s, table = t, paid = p});
        }
        public void Details(string s, string d, string n, double p)
        {
            int i;
            i = deliveryorders.Count;
            deliveryorders.Add(new Order { server = s, driver = d, name = n, paid = p });
        }

    }
    public class SitinOrder : Order
    {
        private int table;
        //public List<Menu> items = new List<Menu>();
        public SitinOrder()
        {

        }
        public int Table
        {
            get { return table; }
            set
            {

                table = value;
            }
        }
        public void Dishes(Menu item)
        {
            items.Add(item);

        }
    }
    public class deliveryOrder : Order
    {
        private string name;
        private string address;
        //public List<Menu> items = new List<Menu>();
        public deliveryOrder()
        {

        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public void Dishes(Menu item)
        {
            items.Add(item);

        }
        public void Display(int i)
        {

        }
    }
}