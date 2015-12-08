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
    public class Employee
    {
        private string name;
        private int id;
        public List<int> used_ids = new List<int>();       //list of used ids
        public Employee()
        {

        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }

    public class Server : Employee
    {
        public Server(string n, int id)
        {
            Name = n;
            ID = id;
        }

        
    }

    public class Driver : Employee
    {
        public string reg;

        public Driver(string n, int id, string r)
        {
            Name = n;
            ID = id;
            reg = r;
        }
    }
}

