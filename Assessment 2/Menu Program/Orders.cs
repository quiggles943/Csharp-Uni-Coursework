using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using System.Collections;
namespace Menu_Program
{
    public class SitinOrder
    {
        private int table;
        public List<Menu> items = new List<Menu>();
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
    public class deliveryOrder
    {
        private string name;
        private string address;
        private int count = 0;
        public List<Menu> items = new List<Menu>();
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