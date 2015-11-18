using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Program
{
    class reader_writer
    {
        string menupath = @"..\..\menu.ini";
        string serverpath = @"..\..\server.ini";
        string sitin_orderpath = @"..\..\sitin_orderlog.ini";
        string delivery_orderpath = @"..\..\delivery_orderlog.ini";
        string driverpath = @"..\..\driver.ini";
        string menufilepath;
        string serverfilepath;
        string driverfilepath;
        string sitin_order_filepath;
        string delivery_order_filepath;

        public int menulength;
        public int serverlength;
        public int driverlength;
        public int sitinlength;
        public int deliverylength;

        string sitin;
        string delivery;
        public string[,] menu = new string[100, 5];
        public string[,] sit = new string[100, 20];
        public string[,] driver = new string[100, 3];
        public string[,] deliver = new string[100, 20];
        public int[] count = new int[100];
        public string[,] server = new string[100, 2];

        
        //MainWindow main = new MainWindow();
        public List<Menu> menuitems = new List<Menu>();
        public List<string[,]> servers = new List<string[,]>();
        public reader_writer()
        {
            
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            driverfilepath = System.IO.Path.GetFullPath(driverpath);
            sitin_order_filepath = System.IO.Path.GetFullPath(sitin_orderpath);
            delivery_order_filepath = System.IO.Path.GetFullPath(delivery_orderpath);
            menu = Menu;
            server = Server;
            driver = Driver;

        }

        public string[,] Menu
        {
            get
            {
                int filelength = 0;
                using (StreamReader r = new StreamReader(menufilepath))
                {
                    while (r.ReadLine() != null) { filelength++; }
                }
                int i = 1;
                string[] file = System.IO.File.ReadAllLines(menufilepath);
                int len = file.Length;
                while (i < (len))
                {
                    string[] column = file[i].Split('\t');
                    int j = 0;
                    while (j < (column.Length))
                    {
                        string buffer = column[j];
                        menu[i, j] = buffer;
                        j++;
                    }
                    bool theanswer = false;
                    switch (menu[i,2])
                    {
                        case "Y": theanswer = true; break;
                        case "N": theanswer = false; break;
                    }
                menuitems.Add(new Menu(menu[i, 0], theanswer, Int32.Parse(menu[i, 1])));
                i++;
                }
                menulength = filelength;
                filelength = 0;
                return menu;
            }
            set
            {
                string[] blank = new string[0];
                File.WriteAllLines(menufilepath, blank);
                StreamWriter file = File.AppendText(menufilepath);
                file.WriteLine("Name\tID\tVegetarian");
                for (int i = 1; i <= menulength; i++)
                {
                    file.WriteLine(menu[i, 0] + "\t" + menu[i, 1] + "\t" + menu[i, 2]);
                }
                file.Close();
            
            }
        }
        
        public string[,] Server
        {
            get
            {
                //read in server text file
                int filelength = 0;
                StreamReader r = new StreamReader(serverfilepath);
                using (r)
                {
                    while (r.ReadLine() != null) { filelength++; }
                }
                int i = 1;
                string[] file = System.IO.File.ReadAllLines(serverfilepath);
                int len = file.Length;
                while (i < (len))
                {
                    string[] column = file[i].Split('\t');
                    int j = 0;
                    while (j < (column.Length))
                    {
                        string buffer = column[j];
                        server[i, j] = buffer;
                        j++;
                    }
                    i++;
                }
                serverlength = filelength;
                filelength = 0;
                //r.Close();
                return server;
            }
            set
            {
                string[] empty = new string[0];
                File.WriteAllLines(serverfilepath, empty);
                StreamWriter logfile = File.AppendText(serverfilepath);
                logfile.WriteLine("Name\tID");
                for (int i = 1; i <= serverlength; i++)
                {
                    logfile.WriteLine(server[i, 0] + "\t" + server[i, 1]);
                }
                logfile.Close();
            }
        }

        public int Serverlength
        {
            get { return serverlength; }
            set { serverlength = value; }
        }
        public string[,] Driver
        {
            get
            {
                int filelength = 0;
                StreamReader r = new StreamReader(driverfilepath);
                using (r)
                {
                    while (r.ReadLine() != null) { filelength++; }
                }
                int i = 1;
                string[] file = System.IO.File.ReadAllLines(driverfilepath);
                int len = file.Length;
                while (i < (len))
                {
                    string[] column = file[i].Split('\t');
                    int j = 0;
                    while (j < (column.Length))
                    {
                        string buffer = column[j];
                        driver[i, j] = buffer;
                        j++;
                    }
                    i++;
                }
                driverlength = filelength;
                filelength = 0;
                return driver;
                //r.Close();
            }
        }

        public string[,] readin_sitin
        {
            get
            {
                if (File.Exists(sitin_order_filepath))
                {
                    int filelength = 0;
                    using (StreamReader r = new StreamReader(sitin_order_filepath))
                    {
                        while (r.ReadLine() != null) { filelength++; }
                    }
                    int i = 1;
                    string[] file = System.IO.File.ReadAllLines(sitin_order_filepath);
                    int len = file.Length;
                    while (i < (len))
                    {
                        string[] column = file[i].Split('\t');
                        int j = 0;
                        while (j < (column.Length))
                        {
                            string buffer = column[j];
                            sit[i, j] = buffer;
                            if (j > 2)
                            {
                                for (int m = 1; m <= menulength; m++)
                                {
                                    if (column[j] == menu[m, 0])
                                    {
                                        count[m]++;
                                    }
                                }
                            }
                            j++;
                        }
                        i++;
                    }
                    sitinlength = filelength;
                    return sit;
                }
                else
                    throw new ArgumentException("No sitin log detected");
            }
        }

        public string[,] readin_delivery
        {
            get
            {
                if (File.Exists(delivery_order_filepath))
                {
                    int filelength = 0;
                    using (StreamReader r = new StreamReader(delivery_order_filepath))
                    {
                        while (r.ReadLine() != null) { filelength++; }
                    }
                    int i = 1;
                    string[] file = System.IO.File.ReadAllLines(delivery_order_filepath);
                    int len = file.Length;
                    while (i < (len))
                    {
                        string[] column = file[i].Split('\t');
                        int j = 0;
                        while (j < (column.Length))
                        {
                            string buffer = column[j];
                            deliver[i, j] = buffer;
                            if (j > 2)
                            {
                                for (int m = 1; m <= menulength; m++)
                                {
                                    if (column[j] == menu[m, 0])
                                    {
                                        count[m]++;
                                    }
                                }
                            }
                            j++;
                        }
                        i++;
                    }
                    deliverylength = filelength;
                    return deliver;
                }
                else
                    throw new ArgumentException("No delivery log detected");
            }
        }
    }
}

