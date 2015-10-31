using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
namespace Menu_Program
{
    //Created by Paul Quigley
    //Class is a student with relevant properties to hold information for the class
    //Last Modified: 20/10/2015
    public class Menu
    {
        private string description;
        private Boolean veg;
        private int price;
        public Menu()
        {

        }
        public Menu(string desc, bool veg, int price)
        {
            Description = desc;
            Vegetarian = veg;
            Price = price;
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public Boolean Vegetarian
        {
            get { return veg; }
            set { veg = value; }
        }
        public int Price
        {
            get { return price; }
            set { price = value;}
        }

    }
    public class SitinOrder : Menu
    {
        private int table;
        private Object[] items;
        public SitinOrder()
        {

        }
        public SitinOrder(string desc, bool veg, int price)
        {
            Description = desc;
            Vegetarian = veg;
            Price = price;
        }
        public int Table
        {
            get { return table; }
            set
            {

                table = value; 
            }
        }
        public Object[] Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }
    }
    public class deliveryOrder : Menu
    {
        private string name;
        private string address;
        private Object[] items;
        public deliveryOrder()
        {

        }
        public deliveryOrder(string desc, bool veg, int price)
        {
            Description = desc;
            Vegetarian = veg;
            Price = price;
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
        public Object[] Items
        {
            get { return items; }
            set
            {
                items = value;
            }
        }
    }
}
