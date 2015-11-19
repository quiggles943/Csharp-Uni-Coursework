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
        //public string[,] menu = new string[100, 5];
        public string[,] sit = new string[100, 20];
        public string[,] driver = new string[100, 3];
        public string[,] deliver = new string[100, 20];
        public int[] count = new int[100];
        public string[,] server = new string[100, 2];

        
        //MainWindow main = new MainWindow();
        public List<Menu> menuitems = new List<Menu>();
        public List<Server> servers = new List<Server>();
        public List<Driver> drivers = new List<Driver>();
        Employee e = new Employee();
        public reader_writer()
        {
            
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            driverfilepath = System.IO.Path.GetFullPath(driverpath);
            sitin_order_filepath = System.IO.Path.GetFullPath(sitin_orderpath);
            delivery_order_filepath = System.IO.Path.GetFullPath(delivery_orderpath);

        }

        public void MenuRead()
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
                    //menu[i, j] = buffer;
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

        public void MenuWrite()
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
        
        public void ServerRead()
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
                    servers.Add(new Server(server[i, 0], Int32.Parse(server[i, 1])));
                    i++;
                }
                serverlength = servers.Count;
                filelength = 0;
                //r.Close();
            }

            public void ServerWrite()
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


        public void DriverRead()
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
                    drivers.Add(new Driver(driver[i, 0], Int32.Parse(driver[i, 1]), driver[i, 2]));
                    i++;
                }
                driverlength = drivers.Count;
                filelength = 0;
                //r.Close();
            }

        public void DriverWrite()
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

        public void readin_sitin()
            {
                bool complete = false;
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
                        i++;
                    }
                    sitinlength = filelength;
                }
                else
                    throw new ArgumentException("No sitin log detected");
            }

        public string[,] readin_delivery
        {
            get
            {
                bool complete = false;
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
                        i++;
                    }
                    deliverylength = filelength;
                    return deliver;
                }
                else
                    throw new ArgumentException("no delivery log detected");
            }
        }
    }
}

