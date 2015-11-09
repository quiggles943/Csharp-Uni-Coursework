using System;
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
using System.Windows.Shapes;

namespace Menu_Program
{
    /// <summary>
    /// Interaction logic for Managerwindow.xaml
    /// </summary>
    public partial class Managerwindow : Window
    {
        string sitin;
        string delivery;
        string[,] menu = new string[100, 5];
        string[,] sit = new string[100, 20];
        string[,] driver = new string[100, 3];
        string[,] deliver = new string[100, 20];
        int[] count = new int[100];
        string[,] servers = new string[100, 2];
        int menulength;
        int sitinlength;
        int deliverylength;
        string menupath = @"..\..\menu.txt";
        string serverpath = @"..\..\server.txt";
        string driverpath = @"..\..\driver.txt";
        string menufilepath;
        string serverfilepath;
        string driverfilepath;
        int serverlength;
        int driverlength;
        List<Menu> menuitems = new List<Menu>();
        public Managerwindow(string delivery_filepath, string sitin_filepath, string[,] menuitems, int length, int slength)
        {
            InitializeComponent();
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            sitin = sitin_filepath;
            delivery = delivery_filepath;
            menu = menuitems;
            menulength = length;
            serverlength = slength;
            readin_delivery();
            readin_sitin();
            readinservers();          
            vegetarianbox.MaxLength = 1;
            start();
        }

        private void readin_delivery()      //reads in delivery orders from file
        {
            int filelength = 0;
            using (StreamReader r = new StreamReader(delivery))
            {
                while (r.ReadLine() != null) { filelength++; }
            }
            int i = 1;
            string[] file = System.IO.File.ReadAllLines(delivery);
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
        }
        private void readin_sitin()     //reads in sit in orders from file
        {
            int filelength = 0;
            using (StreamReader r = new StreamReader(sitin))
            {
                while (r.ReadLine() != null) { filelength++; }
            }
            int i = 1;
            string[] file = System.IO.File.ReadAllLines(sitin);
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
        }

        private void readin_menu()      //reads in menu from file
        {
            //read in menu text file
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
                switch (menu[i, 2])
                {
                    case "Y": theanswer = true; break;
                    case "N": theanswer = false; break;
                }
                menuitems.Add(new Menu(menu[i, 0], theanswer, Int32.Parse(menu[i, 1])));

                i++;
            }
            menulength = filelength;
            filelength = 0;
        }

        private void readinservers()        //reads in servers from file
        {
            //read in server text file
            serverbox.Items.Clear();
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
                    servers[i, j] = buffer;
                    j++;
                }
                serverbox.Items.Add(servers[i, 0]);
                i++;
            }
            serverlength = filelength;
            filelength = 0;
        }

        public void readindrivers()
        {
            //read in driver text file
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

                    i++;
                }
                driverlength = filelength;
                //r.Close();
            }
        }

        private void writeservers()     //writes server changes to file
        {

                if (addrbtn.IsChecked == true)
                {
                        using (StreamWriter logfile = File.AppendText(serverfilepath))
                        logfile.WriteLine(servers[serverlength, 0] + "\t" + servers[serverlength, 1]);
                }
                else if (editrbtn.IsChecked  == true)
                {
                    string[] empty = new string[0];
                    File.WriteAllLines(serverfilepath, empty);
                    StreamWriter logfile = File.AppendText(serverfilepath);
                    logfile.WriteLine("Name\tID");
                    for (int i = 1; i <= serverlength-1; i++)
                    {
                        logfile.WriteLine(servers[i, 0] + "\t" + servers[i, 1]);
                    }
                    logfile.Close();
                }
                else if (removerbtn.IsChecked  == true)
                {
                    string[] empty = new string[0];
                    File.WriteAllLines(serverfilepath, empty);
                    StreamWriter logfile = File.AppendText(serverfilepath);
                    logfile.WriteLine("Name\tID");
                    for (int i = 1; i <= serverlength-2; i++)
                    {
                        logfile.WriteLine(servers[i, 0] + "\t" + servers[i, 1]);
                    }
                    logfile.Close();
                }
                readinservers();
        }

        private void writemenu()        //writes menu changes to file
        {
            if (addrbtn.IsChecked == true)
            {
                using (StreamWriter logfile = File.AppendText(menufilepath))
                    logfile.WriteLine(menu[menulength, 0] + "\t" + menu[menulength, 1] + "\t" + menu[menulength, 2]);
            }
            else if (editrbtn.IsChecked == true)
            {
                string[] empty = new string[0];
                File.WriteAllLines(menufilepath, empty);
                StreamWriter logfile = File.AppendText(menufilepath);
                logfile.WriteLine("Name\tID\tVegetarian");
                for (int i = 1; i <= menulength - 1; i++)
                {
                    logfile.WriteLine(menu[i, 0] + "\t" + menu[i, 1]+ "\t" + menu[i,2]);
                }
                logfile.Close();
            }
            else if (removerbtn.IsChecked == true)
            {
                string[] empty = new string[0];
                File.WriteAllLines(menufilepath, empty);
                StreamWriter logfile = File.AppendText(menufilepath);
                logfile.WriteLine("Name\tID\tVegetarian");
                for (int i = 1; i <= menulength - 2; i++)
                {
                    logfile.WriteLine(menu[i, 0] + "\t" + menu[i, 1] + "\t" + menu[i, 2]);
                }
                logfile.Close();
            }
            readin_menu();
        }

        private void start()        //sets visibility for items on page
        {
            namelabel.Visibility = Visibility.Hidden;
            namebox.Clear();
            namebox.Visibility = Visibility.Hidden;
            staffidlabel.Visibility = Visibility.Hidden;
            staffidbox.Clear();
            staffidbox.Visibility = Visibility.Hidden;
            vegetarianlabel.Visibility = Visibility.Hidden;
            vegetarianbox.Clear();
            vegetarianbox.Visibility = Visibility.Hidden;
            addrbtn.Visibility = Visibility.Hidden;
            editrbtn.Visibility = Visibility.Hidden;
            removerbtn.Visibility = Visibility.Hidden;
            item_selection.Visibility = Visibility.Hidden;
            edit_selection.SelectedIndex = -1;

        }
        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sitinbtn_Click(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            for (int i = 1; i <= sitinlength; i++)
            {
                testlistbox.Items.Add(sit[i, 0] + " " + sit[i, 1] + " " + sit[i, 2]);       //adds sit in orders to item box
            }
        }


        private void total_itemsbtn_Click(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            for( int i=1; i< menulength; i++)
            {
                testlistbox.Items.Add(menu[i, 0] + " " + count[i]);         //adds total amount of each item ordered to item box
            }
        }

        private void deliverybtn_Click_1(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            for (int i = 1; i <= deliverylength; i++)
            {
                testlistbox.Items.Add(deliver[i, 0] + " " + deliver[i, 2] + " " + deliver[i, 3]);       //adds delivery orders to item box
            }
        }

        private void serverbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serverbox.SelectedIndex == -1)
            {
                serverbtn.IsEnabled = false;
                serverbtn.Content = "Please select server";
            }

            else
            {
                serverbtn.IsEnabled = true;
                serverbtn.Content = "Show orders for " + serverbox.SelectedItem.ToString();
            }
        }

        private void serverbtn_Click(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();

            for (int i = 1; i <= sitinlength; i++)
            {
                if (serverbox.SelectedItem.ToString() == sit[i, 0])
                {
                    testlistbox.Items.Add(sit[i, 1] + " " + sit[i, 2]);
                }
            }
                for (int j = 1; j <= deliverylength; j++)
                {
                    if (serverbox.SelectedItem.ToString() == deliver[j, 0])
                {
                    testlistbox.Items.Add(deliver[j, 2] + " " + deliver[j, 3]);
                }

            }
        }

        private void edit_selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item_selection.Items.Clear();
            if (edit_selection.SelectedIndex != -1)
            {
                addrbtn.Visibility = Visibility.Visible;
                editrbtn.Visibility = Visibility.Visible;
                removerbtn.Visibility = Visibility.Visible;
            }
            if (edit_selection.SelectedIndex == 0)      //Servers
            {
                for (int i = 1; i <= (serverlength-1); i++)
                {
                    item_selection.Items.Add(servers[i, 0]);
                }
                staffidlabel.Content = "Staff Id";
            }

            if (edit_selection.SelectedIndex == 1)      //Drivers
            {

            }
            if(edit_selection.SelectedIndex == 2)       //Menu Items
            {
                vegetarianbox.Clear();
                for (int i = 1; i <= menulength; i++)
                {
                    item_selection.Items.Add(menu[i, 0]);
                }
                staffidlabel.Content = "Price £";
            }
            else
            {
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
                staffidlabel.Content = "Staff Id";
            }
            addrbtn.IsChecked = false;
            editrbtn.IsChecked = false;
            removerbtn.IsChecked = false;
            vegetarianbox.Clear();
        }

        private void addrbtn_Checked(object sender, RoutedEventArgs e)
        {
            namelabel.Visibility = Visibility.Visible;
            namebox.Visibility = Visibility.Visible;
            staffidlabel.Visibility = Visibility.Visible;
            staffidbox.Visibility = Visibility.Visible;
            item_selection.Visibility = Visibility.Hidden;
        }

        private void editrbtn_Checked(object sender, RoutedEventArgs e)
        {
            namebox.Text = "";
            staffidbox.Text = "";
            item_selection.Visibility = Visibility.Visible;
            namelabel.Visibility = Visibility.Visible;
            namebox.Visibility = Visibility.Visible;
            staffidlabel.Visibility = Visibility.Visible;
            staffidbox.Visibility = Visibility.Visible;
        }

        private void removerbtn_Checked(object sender, RoutedEventArgs e)
        {
            namelabel.Visibility = Visibility.Hidden;
            namebox.Visibility = Visibility.Hidden;
            staffidlabel.Visibility = Visibility.Hidden;
            staffidbox.Visibility = Visibility.Hidden;
            namebox.Text = "";
            staffidbox.Text = "";
            item_selection.Visibility = Visibility.Visible;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (edit_selection.SelectedIndex == 0)      //Servers
            {
                if (editrbtn.IsChecked == true)
                {
                    for (int i = 1; i <= ((servers.Length / 2) - 1); i++)
                    {
                        if (servers[i, 0] == item_selection.SelectedItem.ToString())
                        {
                            servers[i, 0] = namebox.Text;
                            servers[i, 1] = Int32.Parse(staffidbox.Text).ToString();
                        }
                    }
                    writeservers();

                }
                if (addrbtn.IsChecked == true)
                {
                    servers[serverlength, 0] = namebox.Text;
                    servers[serverlength, 1] = Int32.Parse(staffidbox.Text).ToString();
                    writeservers();
                }
                if (removerbtn.IsChecked == true)
                {
                    for (int i = 1; i <= serverlength; i++)
                    {
                        if (servers[i, 0] == item_selection.SelectedItem.ToString())
                        {
                            for (int j = i; j <= serverlength; j++)
                            {
                                servers[j, 0] = servers[j + 1, 0];
                                servers[j, 1] = servers[j + 1, 1];
                            }
                        }

                    }
                    writeservers();
                }
            }

            else if (edit_selection.SelectedIndex == 1)     //Drivers
            {

            }

            else if (edit_selection.SelectedIndex == 2)     //Menu Items
            {
                if (editrbtn.IsChecked == true)
                {
                    for (int i = 1; i <= menulength; i++)
                    {
                        if (menu[i, 0] == item_selection.SelectedItem.ToString())
                        {
                            menu[i, 0] = namebox.Text;
                            menu[i, 1] = Int32.Parse(staffidbox.Text).ToString();
                            menu[menulength, 2] = vegetarianbox.Text;
                        }
                    }
                    writemenu();

                }
                if (addrbtn.IsChecked == true)
                {
                    menu[menulength, 0] = namebox.Text;
                    menu[menulength, 1] = Int32.Parse(staffidbox.Text).ToString();
                    menu[menulength, 2] = vegetarianbox.Text;
                    writemenu();
                }
                if (removerbtn.IsChecked == true)
                {
                    for (int i = 1; i <= menulength; i++)
                    {
                        if (menu[i, 0] == item_selection.SelectedItem.ToString())
                        {
                            for (int j = i; j <= serverlength; j++)
                            {
                                menu[j, 0] = menu[j + 1, 0];
                                menu[j, 1] = menu[j + 1, 1];
                                menu[j, 2] = menu[j + 1, 2];
                            }
                        }

                    }
                    writemenu();
                }
            }
            start();

        }

        private void item_selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
