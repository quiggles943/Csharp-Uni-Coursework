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
        private string server;
        private string date;
        public string name;
        
        private double paid;
        private string itemstring;
        string[] orders = new string[10];
        List<string> notes = new List<string>();
        public Order()
        {

        }
        public void Clear()
        {
            items.Clear();
        }
        public string Server
        {
            get { return server; }
            set { server = value; }
        }
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        
        
        public double Paid
        {
            get { return paid; }
            set { paid = value; }
        }

        public void Note(string note)
        {
            notes.Add(note);
        }

        public void addNote(int index, string note)
        {
            notes.RemoveAt(index);
            notes.Insert(index, note);
        }

        public void removeNote(int index)
        {
            notes.RemoveAt(index);
        }

        public string readNote(int index)
        {
            return notes[index];
        }

        public void Dishes(Menu item)
        {
            items.Add(item);

        }
        public void removeDish(Menu item)
        {
            items.Remove(item);
        }

        public string Items
        {
            get { return itemstring; }
            set { itemstring = value; }
        }
    }
    public class sitinOrder: Order
    {
        private int table;
        public sitinOrder()
        {

        }
        public int Table
        {
            get { return table; }
            set { table = value; }
        }
    }
    public class deliveryOrder : Order
    {
        
        private string address;
        public string driver;
        public deliveryOrder()
        {

        }
        public string Driver
        {
            get { return driver; }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("No name entered");
                }
                else
                    driver = value;
            }
        }
        public string Name
        {
            get { return name; }
            set 
            {
                if(value == "")
                {
                    throw new ArgumentException("No name entered");
                }
                else
                name = value; 
            }
        }
        public string Address
        {
            get { return address; }
            set 
            {
                if (value == "")
                {
                    throw new ArgumentException("No address entered");
                }
                else
                address = value;
            }
        }

        public void Dishes(Menu item)
        {
            items.Add(item);

        }

        
    }
}