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
        public string driver;
        public string name;
        private int table;
        private double paid;
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
        public int Table
        {
            get { return table; }
            set { table = value; }
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
    }
    public class sitinOrder : Order
    {
        public sitinOrder()
        {

        }
    }
    public class deliveryOrder : Order
    {
        
        private string address;
        public deliveryOrder()
        {

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