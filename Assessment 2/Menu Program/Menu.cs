using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;
using System.Collections;
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
        private int count;
        private string note;
        public Menu()
        {

        }
        public Menu(string desc, bool veg, int price)
        {
            Description = desc;
            Vegetarian = veg;
            Price = price;
            note = "none";
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
        public int Count
        {
            get { return count; }
            set { count = value;}
        }
        public string Note
        {
            get { return note; }
            set { note = value; }
        }
    }

    
}
