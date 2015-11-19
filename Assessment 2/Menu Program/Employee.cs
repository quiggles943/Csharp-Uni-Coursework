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
        public string name;
        public int ID;

        public Employee()
        {

        }

    }

    public class Server : Employee
    {
        public Server(string n, int id)
        {
            name = n;
            ID = id;
        }
    }

    public class Driver : Employee
    {
        public string reg;

        public Driver(string n, int id, string r)
        {
            name = n;
            ID = id;
            reg = r;
        }
    }
}

