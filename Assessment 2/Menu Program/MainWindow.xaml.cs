﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int filelength = 0;
        public string[,] menu = new string[100,5];
        public string[,] server = new string[100, 2];
        public string[,] driver = new string[100, 3];
        string menupath = @"..\..\menu.txt";
        string serverpath = @"..\..\server.txt";
        string sitin_orderpath = @"..\..\sitin_orderlog.txt";
        string delivery_orderpath = @"..\..\delivery_orderlog.txt";
        string driverpath = @"..\..\driver.txt";
        string menufilepath;
        string serverfilepath;
        string driverfilepath;
        string sitin_order_filepath;
        string delivery_order_filepath;
        string items;
        int menulength;
        int serverlength;
        int driverlength;
        bool sitin;
        double subtotal;
        int table;
        int itemsordered = 0;
        string missingfiles = "";
        List<Menu> menuitems = new List<Menu>();
        Order o = new Order();
        deliveryOrder d = new deliveryOrder();
        SitinOrder s = new SitinOrder();
        public List<Order> sitinorders = new List<Order>();
        bool detailsadded = false;
        //int denotes number of tables in restaraunt
        int tablenos = 20;
        
        public MainWindow()
        {
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            driverfilepath = System.IO.Path.GetFullPath(driverpath);
            sitin_order_filepath = System.IO.Path.GetFullPath(sitin_orderpath);
            delivery_order_filepath = System.IO.Path.GetFullPath(delivery_orderpath);
            InitializeComponent();
           /* filesexist();
            if(missingfiles != "")
            {
                Managerwindow manager = new Managerwindow(menu, menulength, serverlength, missingfiles);
                this.Hide();
                manager.ShowDialog();
            }*/
            tablebox.Visibility = Visibility.Hidden;
            tabletxt.Visibility = Visibility.Hidden;
            addtablebtn.Visibility = Visibility.Hidden;
            tablelabel.Visibility = Visibility.Hidden;
            tablebox.Visibility = Visibility.Hidden;
            destinationlabel.Visibility = Visibility.Hidden;
            detailsbtn.Visibility = Visibility.Hidden;
            nametxtbox.Visibility = Visibility.Hidden;
            addresstxtbox.Visibility = Visibility.Hidden;
            namelabel.Visibility = Visibility.Hidden;
            addresslabel.Visibility = Visibility.Hidden;
            driverbox.Visibility = Visibility.Hidden;
            orderlistbox.IsEnabled = false;
            sitinradbtn.IsEnabled = false;
            takeawayradbtn.IsEnabled = false;
            logoutbtn.IsEnabled = false;
            readinservers();
            menureadin();
            readindrivers();
            
        }

        /*private void filesexist()
        {
            string menu;
            string server;
            string driver;
            if(!File.Exists(menufilepath))
                menu = "menu," ;
            else
                menu = "";

            if (!File.Exists(serverfilepath))
                server = "server,";
            else
                server = "";

            if (!File.Exists(driverfilepath))
                driver = "driver";
            else
                driver = "";

            missingfiles = menu+ "  " +server+ "  "+ driver;

        }*/

        private void menureadin()
        {
            //read in menu text file
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
                foodlistbox.Items.Add(menu[i, 0]);
                i++;
            }
            menulength = filelength;
            filelength = 0;
        }
        public void readinservers()
        {
            //read in server text file
            StreamReader r = new StreamReader(serverfilepath);
            using (r)
            {
                while (r.ReadLine() != null) { filelength++; }
            }
            int i = 1;
            string [] file = System.IO.File.ReadAllLines(serverfilepath);
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
                serverlist.Items.Add(server[i, 0]);
                i++;
            }
            serverlength = filelength;
            filelength = 0;
            //r.Close();
        }

        public void readindrivers()
        {
            //read in driver text file
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
                driverbox.Items.Add(driver[i, 0]);
                i++;
            }
            driverlength = filelength;
            filelength = 0;
            //r.Close();
        }

        private void writetofile(string server, int table, double paid, string items)     //writes to sit in order log 
        {
            if (File.Exists(sitin_order_filepath))
            {

                using (StreamWriter logfile = File.AppendText(sitin_order_filepath))
                {

                    logfile.WriteLine(server + "\t" + table + "\t£" + paid+"\t"+items);
                }
            }
            else
            {
                using (StreamWriter logfile = File.CreateText (sitin_order_filepath))
                {
                    logfile.WriteLine("Server\tTable\tAmountpaid");
                    logfile.WriteLine(server + "\t" + table + "\t£" + paid+"\t"+items);
                }
            }

        }
        private void writetofile(string server, string driver, string name, double paid, string items)        //writes to delivery order log 
        {
            if (File.Exists(delivery_order_filepath))
            {

                using (StreamWriter logfile = File.AppendText(delivery_order_filepath))
                {

                    logfile.WriteLine(server + "\t" + driver + "\t" + name + "\t£" + paid + "\t" + items);
                }
            }
            else
            {
                using (StreamWriter logfile = File.CreateText(delivery_order_filepath))
                {
                    logfile.WriteLine("Server\tDriver\tCust Name\tAmountpaid");
                    logfile.WriteLine(server + "\t" + driver + "\t" + name + "\t£" + paid + "\t" + items);
                }
            }

        }

        private void serverlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void logonbtn_Click(object sender, RoutedEventArgs e)
        {
            if (serverlist.SelectedIndex != -1)
            {
                serverlist.IsEnabled = false;
                serverstatusbox.Content = serverlist.SelectedItem;
                sitinradbtn.IsEnabled = true;
                takeawayradbtn.IsEnabled = true;
                logoutbtn.IsEnabled = true;
                logonbtn.IsEnabled = false;
                selectbtn.IsEnabled = true;
                statuslabel.Content = "Logged on";
            }
            else
                return;

        }

        private void foodlistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (foodlistbox.SelectedIndex == -1)
            {
                orderlistbox.IsEnabled = false;
            }
            else
                orderlistbox.IsEnabled = true;

        }

        private void addtobtn_Click(object sender, RoutedEventArgs e)
        {
            if (foodlistbox.SelectedIndex == -1)
                return;
            double buffer = 0;
            if (sitin)
            {
                buffer = menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                s.Dishes(menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()));
            }
            else
            {
                buffer = menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                d.Dishes(menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()));
            }
            orderlistbox.Items.Add(foodlistbox.SelectedItem);
            subtotal  = subtotal + Math.Round(buffer,2);
            subtotallabel.Content = subtotal;
            items = string.Join("\t", items, foodlistbox.SelectedItem.ToString());
            itemsordered++;
            statuslabel.Content = "Item added";
        }

        private void sitinradbtn_Checked(object sender, RoutedEventArgs e)
        {
            sitin = true;   
        }

        private void takeawayradbtn_Checked(object sender, RoutedEventArgs e)
        {
            sitin = false;
            tablebox.Visibility = Visibility.Hidden;
            tabletxt.Visibility = Visibility.Hidden;
            tabletxt.Content = "";
            destinationlabel.Content = "";
            
            
        }

        private void selectbtn_Click(object sender, RoutedEventArgs e)
        {

            if (sitin)
            {
                addtablebtn.Visibility = Visibility.Visible;
                tablelabel.Visibility = Visibility.Visible;
                tablebox.Visibility = Visibility.Visible;
                nametxtbox.Visibility = Visibility.Hidden;
                nametxtbox.Text = "";
                addresstxtbox.Visibility = Visibility.Hidden;
                addresstxtbox.Text = "";
                namelabel.Visibility = Visibility.Hidden;
                addresslabel.Visibility = Visibility.Hidden;
                statuslabel.Content = "Sit-in selected";
                tabletxt.Visibility = Visibility.Visible;
                destinationlabel.Visibility = Visibility.Visible;
                destinationlabel.Content = "Table:";
                
                
            }
            else
            {
                detailsbtn.Visibility = Visibility.Visible;
                nametxtbox.Visibility = Visibility.Visible;
                addresstxtbox.Visibility = Visibility.Visible;
                namelabel.Visibility = Visibility.Visible;
                addresslabel.Visibility = Visibility.Visible;
                driverbox.Visibility = Visibility.Visible;
                nametxtbox.IsEnabled = true;
                addresstxtbox.IsEnabled = true;
                statuslabel.Content = "Delivery selected";

            }
            destinationlabel.Visibility = Visibility.Visible;
            addtobtn.IsEnabled = true;
            sitinradbtn.IsEnabled = false;
            takeawayradbtn.IsEnabled = false;
            
        }

        private void addtablebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(tablebox.Text) < 1 || Int32.Parse(tablebox.Text) > tablenos)
                    throw new ArgumentException("Table number invalid");
                table = Int32.Parse(tablebox.Text);
                tabletxt.Content = table;
                statuslabel.Content = "Table selected";
                foodlistbox.IsEnabled = true;
                addtablebtn.IsEnabled = false;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "error");
            }
        }

        private void clearbtn_Click(object sender, RoutedEventArgs e)
        {
            tablebox.Clear();
            subtotal = 0;
            subtotallabel.Content = "0.00";
            table = 0;
            tabletxt.Content = "";
            tablelabel.Visibility = Visibility.Hidden;
            addtablebtn.Visibility = Visibility.Hidden;
            tablelabel.Visibility = Visibility.Hidden;
            tablebox.Visibility = Visibility.Hidden;
            destinationlabel.Visibility = Visibility.Hidden;
            addtablebtn.IsEnabled = true;
            sitinradbtn.IsEnabled = true;
            takeawayradbtn.IsEnabled = true;
            //foodlistbox.Items.Clear();
            foodlistbox.IsEnabled = false;
            orderlistbox.Items.Clear();
            nametxtbox.Visibility = Visibility.Hidden;
            nametxtbox.Text = "";
            addresstxtbox.Visibility = Visibility.Hidden;
            addresstxtbox.Text = "";
            namelabel.Visibility = Visibility.Hidden;
            addresslabel.Visibility = Visibility.Hidden;
            nametxtbox.IsEnabled = true;
            addresstxtbox.IsEnabled = true;
            detailsbtn.Visibility = Visibility.Hidden;
            detailsadded = false;
            d.Clear();
            s.Clear();
            driverbox.Visibility = Visibility.Hidden;
            driverbox.SelectedIndex = -1;
            statuslabel.Content = "Order cleared";

        }

        private void totalbtn_Click(object sender, RoutedEventArgs e)
        {
            orderlistbox.IsEnabled = false;
            foodlistbox.IsEnabled = false;
            addtobtn.IsEnabled = false;
            statuslabel.Content = "Subtotal generated";

        }

        private void detailsbtn_Click(object sender, RoutedEventArgs e)
        {
            if (detailsadded)
                return;
            try
            {
                if(driverbox.SelectedIndex == -1)
                    throw new ArgumentException("No driver selected");
                d.Driver = driverbox.SelectedItem.ToString();
                d.Name = nametxtbox.Text;
                d.Address = addresstxtbox.Text;
                foodlistbox.IsEnabled = true;
                addtablebtn.Visibility = Visibility.Hidden;
                tablelabel.Visibility = Visibility.Hidden;
                tablebox.Visibility = Visibility.Hidden;
                d.Name = nametxtbox.Text;
                d.Address = addresstxtbox.Text;
                destinationlabel.Content = "Name";
                tabletxt.Content = nametxtbox.Text;
                nametxtbox.IsEnabled = false;
                addresstxtbox.IsEnabled = false;
                detailsadded = true;
                statuslabel.Content = "Details added";
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "error");
            }

        }

        private void nametxtbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void billbtn_Click(object sender, RoutedEventArgs e)
        {
            if (sitin)
            {
                writetofile(serverlist.SelectedItem.ToString(), table, subtotal, items);
                s.Paid = subtotal;
                Window Bill = new Bill(s, sitin);
                Bill.ShowDialog();
                //o.Details(sitinorders,serverlist.SelectedItem.ToString(), table, subtotal);
            }
            else
            {
                string driver = driverbox.SelectedItem.ToString();
                d.Paid = subtotal;
                writetofile(serverlist.SelectedItem.ToString(), driver,  nametxtbox.Text, subtotal, items);
                Window Bill = new Bill(d);
                Bill.ShowDialog();
            }
            statuslabel.Content = "Bill printed";
            

        }

        private void logoutbtn_Click(object sender, RoutedEventArgs e)
        {
            clearbtn_Click(sender, e);
            selectbtn.IsEnabled = false;
            sitinradbtn.IsEnabled = false;
            takeawayradbtn.IsEnabled = false;
            serverlist.IsEnabled = true;
            logoutbtn.IsEnabled = false;
            logonbtn.IsEnabled = true;
            statuslabel.Content = "Logged out";
        }

        private void managerbtn_Click(object sender, RoutedEventArgs e)
        {
            Managerwindow manager = new Managerwindow(menu, menulength, serverlength, missingfiles);
            manager.ShowDialog();
        }


    }
}
