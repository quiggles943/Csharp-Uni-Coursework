using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Program
{
    public class reader_writer
    {
        string menupath = @"..\..\menu.ini";
        string serverpath = @"..\..\server.ini";
        string sitin_orderpath = @"..\..\sitin_orderlog.ini";
        string delivery_orderpath = @"..\..\delivery_orderlog.ini";
        string driverpath = @"..\..\driver.ini";
        string idpath = @"..\..\used_ids.ini";
        string menufilepath;
        string serverfilepath;
        string driverfilepath;
        string sitin_order_filepath;
        string delivery_order_filepath;
        string idlistfilepath;

        public int menulength;
        public int serverlength;
        public int driverlength;
        public int sitinlength;
        public int deliverylength;
        public int usedidlength;
        public bool sitinfound = false;
        public bool deliveryfound = false;

        public string[,] sit = new string[100, 20];
        public string[,] deliver = new string[100, 20];
        public int[] count = new int[100];

        public int longsitin = 0;
        int longdeliver;

        
        public List<Menu> menuitems = new List<Menu>();         //list of menu items
        public List<Server> servers = new List<Server>();       //list of servers
        public List<Driver> drivers = new List<Driver>();       //list of drivers
        public List<int> used_ids = new List<int>();       //list of used ids
        public reader_writer()      //initialises reader writer class
        {
            
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            driverfilepath = System.IO.Path.GetFullPath(driverpath);
            sitin_order_filepath = System.IO.Path.GetFullPath(sitin_orderpath);
            delivery_order_filepath = System.IO.Path.GetFullPath(delivery_orderpath);
            idlistfilepath = System.IO.Path.GetFullPath(idpath);
        }

        public void MenuRead()      //reads from menu file
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
                    j++;
                }
                bool theanswer = false;
                switch (column[2])
                {
                    case "Y": theanswer = true; break;
                    case "N": theanswer = false; break;
                }
                menuitems.Add(new Menu(column[0], theanswer, Int32.Parse(column[1])));
                i++;
            }
            menulength = menuitems.Count;
            filelength = 0;
        }

        public void MenuWrite()     //writes to menu file
        {
            string[] blank = new string[0];
            File.WriteAllLines(menufilepath, blank);
            StreamWriter file = File.AppendText(menufilepath);
            file.WriteLine("Name\tID\tVegetarian");
            foreach (var item in menuitems)
            {
                file.WriteLine(item.Description + "\t" + item.Price + "\t" + item.Vegetarian);
            }
            file.Close();
        }
        
        public void ServerRead()        //reads from server file
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
                        
                        j++;
                    }
                    servers.Add(new Server(column[0], Int32.Parse(column[1])));
                    i++;
                }
                serverlength = servers.Count;
                filelength = 0;
            }

            public void ServerWrite()       //writes to server file
            {
                var sortedservers = servers.OrderBy(x => x.ID);
                servers = sortedservers.ToList();
                string[] empty = new string[0];
                File.WriteAllLines(serverfilepath, empty);
                StreamWriter logfile = File.AppendText(serverfilepath);
                logfile.WriteLine("Name\tID");
                foreach (var item in servers)
                {
                    logfile.WriteLine(item.name + "\t" + item.ID);
                }
                logfile.Close();
            }


        public void DriverRead()        //reads from driver file
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
                        //driver[i, j] = buffer;
                        j++;
                    }
                    drivers.Add(new Driver(column[0], Int32.Parse(column[1]), column[2]));
                    //drivers.Add(new Driver(column[0], Int32.Parse(column[1]), column[2]));
                    i++;
                }
                driverlength = drivers.Count;
                filelength = 0;
                //r.Close();
            }

        public void DriverWrite()       //writes to driver file
        {
            var sorteddrivers = drivers.OrderBy(x => x.ID);
            drivers = sorteddrivers.ToList();
            string[] empty = new string[0];
            File.WriteAllLines(driverfilepath, empty);
            StreamWriter logfile = File.AppendText(driverfilepath);
            logfile.WriteLine("Name\tID\tReg");
            foreach (var item in drivers)
            {
                logfile.WriteLine(item.name + "\t" + item.ID + "\t" +item.reg);
            }
            logfile.Close();
        }

        public void readin_sitin()      //reads in sit in orders
            {
                
                
                bool complete = false;
                if (File.Exists(sitin_order_filepath))
                {
                    File.SetAttributes(sitin_order_filepath, FileAttributes.ReadOnly);
                    int filelength = 0;
                    sitinfound = true;
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
                            if (j > 3)
                            {
                                complete = false;
                                for (int m = 0; m <= menuitems.Count; m++)
                                {
                                    if (complete == false)
                                    {
                                        if (column[j] == menuitems[m].Description)
                                        {
                                            menuitems[m].Count = (menuitems[m].Count + 1);
                                            complete = true;
                                        }
                                    }
                                }
                            }
                            j++;
                        }
                        int count = 0;
                        for (int k = 0; k <= 3; k++)
                        {
                            count = count + column[k].Length;
                        }
                        if (count > longsitin)
                            longsitin = count;
                        i++;
                    }
                    sitinlength = filelength;
                    File.SetAttributes(sitin_order_filepath, FileAttributes.Normal);
                }
                else
                {
                    //throw new ArgumentException("No sitin log detected");
                }
            }

        public void readin_delivery()       //reads in delivery orders
        {
                
                bool complete = false;
                if (File.Exists(delivery_order_filepath))
                {
                    File.SetAttributes(delivery_order_filepath, FileAttributes.ReadOnly);
                    int filelength = 0;
                    deliveryfound = true;
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
                            if (j > 4)
                            {
                                complete = false;
                                for (int m = 0; m <= menuitems.Count; m++)
                                {
                                    if (complete == false)
                                    {
                                        if (column[j] == menuitems[m].Description)
                                        {
                                            menuitems[m].Count = (menuitems[m].Count + 1);
                                            complete = true;
                                        }
                                    }
                                }
                            }
                            j++;
                        }
                        if (file.Length > longdeliver)
                            longdeliver = file.Length;
                        i++;
                    }
                    deliverylength = filelength;
                    File.SetAttributes(delivery_order_filepath, FileAttributes.Normal);
                }
                //else
                    //throw new ArgumentException("no delivery log detected");
           
        }
        public void IdListRead()        //reads from Id list file
        {
            int filelength = 0;
            StreamReader r = new StreamReader(idlistfilepath);
            using (r)
            {
                while (r.ReadLine() != null) { filelength++; }
            }
            int i = 1;
            string[] file = System.IO.File.ReadAllLines(idlistfilepath);
            int len = file.Length;
            while (i < (len))
            {
                string[] column = file[i].Split('\t');
                int value = Int32.Parse(file[i]);
                int j = 0;
                while (j < (column.Length))
                {
                    string buffer = column[j];
                    j++;
                }
                used_ids.Add(value);
                i++;
            }
            usedidlength = used_ids.Count;
            filelength = 0;
            //r.Close();
        }
        public void IdListWrite()       //writes to driver file
        {
            used_ids.Sort();
            //used_ids = sortedids.ToList();
            string[] empty = new string[0];
            File.WriteAllLines(idlistfilepath, empty);
            StreamWriter logfile = File.AppendText(idlistfilepath);
            logfile.WriteLine("Used Ids");
            foreach (var item in used_ids)
            {
                logfile.WriteLine(item);
            }
            logfile.Close();
        }

    }
}

