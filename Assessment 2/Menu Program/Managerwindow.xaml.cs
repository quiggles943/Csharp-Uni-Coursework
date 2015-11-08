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
        string[,] deliver = new string[100, 20];
        int[] count = new int[100];
        string[,] servers = new string[100, 2];
        int menulength;
        int sitinlength;
        int deliverylength;
        string menupath = @"..\..\menu.txt";
        string serverpath = @"..\..\server.txt";
        string menufilepath;
        string serverfilepath;
        int serverlength;
        List<Menu> menuitems = new List<Menu>();
        public Managerwindow(string delivery_filepath, string sitin_filepath, string[,] menuitems, int length, string[,] server, int slength)
        {
            InitializeComponent();
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            sitin = sitin_filepath;
            delivery = delivery_filepath;
            menu = menuitems;
            menulength = length;
            readin_delivery();
            readin_sitin();
            servers = server;
            for(int i = 1; i<slength; i++)
            {
                serverbox.Items.Add(servers[i, 0]);
            }
            serverlength = slength;
        }

        private void readin_delivery()
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
                //testlistbox.Items.Add(deliver[i, 0] + " " + deliver[i, 1] + " " + deliver[i, 2]);
                i++;
            }
            deliverylength = filelength;
        }
        private void readin_sitin()
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
                //testlistbox.Items.Add(sit[i, 0]+" " +sit[i,1]+ " " +sit[i,2]);
                i++;
            }
            sitinlength = filelength;
        }

        private void readin_menu()
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

        private void writeservers()
        {
            //using (StreamWriter logfile = File.AppendText(serverfilepath))
            //{
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
            //}
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
                testlistbox.Items.Add(sit[i, 0] + " " + sit[i, 1] + " " + sit[i, 2]);
            }
        }


        private void total_itemsbtn_Click(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            for( int i=1; i< menulength; i++)
            {
                testlistbox.Items.Add(menu[i, 0] + " " + count[i]);
            }
        }

        private void deliverybtn_Click_1(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            for (int i = 1; i <= deliverylength; i++)
            {
                testlistbox.Items.Add(deliver[i, 0] + " " + deliver[i, 2] + " " + deliver[i, 3]);
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
            if (edit_selection.SelectedIndex == 0)
            {
                for (int i = 1; i <= ((servers.Length/2)-1); i++)
                {
                    item_selection.Items.Add(servers[i, 0]);
                }
            }
            if(edit_selection.SelectedIndex == 2)
            {
                vegetarianlabel.Visibility = Visibility.Visible;
                yesrbtn.Visibility = Visibility.Visible;
                norbtn.Visibility = Visibility.Visible;
                yesrbtn.IsChecked = false;
                norbtn.IsChecked = false;
                for (int i = 1; i <= menulength; i++)
                {
                    item_selection.Items.Add(menu[i, 0]);
                }
            }
            else
            {
                vegetarianlabel.Visibility = Visibility.Hidden;
                yesrbtn.Visibility = Visibility.Hidden;
                norbtn.Visibility = Visibility.Hidden;
            }
            addrbtn.IsChecked = false;
            editrbtn.IsChecked = false;
            removerbtn.IsChecked = false;
            yesrbtn.IsChecked = false;
            norbtn.IsChecked = false;
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

        private void item_selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(editrbtn.IsChecked == true)
            {
                for (int i = 1; i <= ((servers.Length/2)-1); i++)
                {
                    if(servers[i,0] == item_selection.SelectedItem.ToString())
                    {
                        servers[i, 0] = namebox.Text;
                        servers[i, 1] = Int32.Parse(staffidbox.Text).ToString();
                    }
                }
                writeservers();
                
            }
            if(addrbtn.IsChecked==true)
            {
                servers[serverlength, 0] = namebox.Text;
                servers[serverlength, 1] = Int32.Parse(staffidbox.Text).ToString();
                writeservers();
            }
            if(removerbtn.IsChecked==true)
            {
                for (int i = 1; i <= serverlength; i++)
                {
                    if (servers[i, 0] == item_selection.SelectedItem.ToString())
                    {
                        for(int j = i; j<= serverlength;j++)
                        {
                            servers[j, 0] = servers[j+1, 0];
                            servers[j, 1] = servers[j+1, 1];
                        }
                    }
                    
                }
                writeservers();
            }
        }
    }
}
