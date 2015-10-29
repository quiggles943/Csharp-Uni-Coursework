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
        string menupath = @"..\..\menu.txt";
        string serverpath = @"..\..\server.txt";
        string menufilepath;
        string serverfilepath;
        bool optionselected = false;
        bool sitin;
        double subtotal;
        int table;
        IDictionary<string, Menu> menuitems = new Dictionary<string, Menu>();
        IDictionary<string, SitinOrder> sitinmenuitems = new Dictionary<string, SitinOrder>();
        IDictionary<string, deliveryOrder> deliverymenuitems = new Dictionary<string, deliveryOrder>();
        public MainWindow()
        {
            InitializeComponent();
            tablebox.Visibility = Visibility.Hidden;
            tabletxt.Visibility = Visibility.Hidden;
            addtablebtn.Visibility = Visibility.Hidden;
            tablelabel.Visibility = Visibility.Hidden;
            tablebox.Visibility = Visibility.Hidden;
            destinationlabel.Visibility = Visibility.Hidden;
            orderlistbox.IsEnabled = false;
            sitinradbtn.IsEnabled = false;
            takeawayradbtn.IsEnabled = false;
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            readinservers();
        }

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
                if(sitin)
                    menuitems[menu[i, 0]] = new SitinOrder(menu[i, 0],theanswer ,Int32.Parse(menu[i, 1])); 
                else
                    menuitems[menu[i, 0]] = new deliveryOrder(menu[i, 0], theanswer, Int32.Parse(menu[i, 1]));
                foodlistbox.Items.Add(menu[i, 0]);
                i++;
            }
            filelength = 0;
        }
        public void readinservers()
        {
            //read in server text file
            using (StreamReader r = new StreamReader(serverfilepath))
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
            buffer = menuitems[foodlistbox.SelectedItem.ToString()].Price;
            buffer = (buffer / 100);
            orderlistbox.Items.Add(foodlistbox.SelectedItem);
            subtotal  = subtotal + buffer;
            subtotallabel.Content = subtotal;
        }

        private void sitinradbtn_Checked(object sender, RoutedEventArgs e)
        {
            sitin = true;
            tablebox.Visibility = Visibility.Visible;
            tabletxt.Visibility = Visibility.Visible;
            destinationlabel.Content = "Table";
             
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
            if (sitin == false)
            {
                menureadin();
                foodlistbox.IsEnabled = true;
                addtablebtn.Visibility = Visibility.Hidden;
                tablelabel.Visibility = Visibility.Hidden;
                tablebox.Visibility = Visibility.Hidden;
            }
            else
            {
                addtablebtn.Visibility = Visibility.Visible;
                tablelabel.Visibility = Visibility.Visible;
                tablebox.Visibility = Visibility.Visible;
                
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
                if (Int32.Parse(tablebox.Text) < 1 || Int32.Parse(tablebox.Text) > 20)
                    throw new ArgumentException("Table number invalid");
                table = Int32.Parse(tablebox.Text);
                tabletxt.Content = table;
                if(sitin)
                {
                    menureadin();
                    foodlistbox.IsEnabled = true;
                }
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
            totallabel.Content = "0.00";
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
            foodlistbox.Items.Clear();
            foodlistbox.IsEnabled = false;
            orderlistbox.Items.Clear();
        }

        private void totalbtn_Click(object sender, RoutedEventArgs e)
        {
            orderlistbox.IsEnabled = false;
            foodlistbox.IsEnabled = false;
            addtobtn.IsEnabled = false;
            if (sitin)
                totallabel.Content = subtotal;
            else
                totallabel.Content = subtotal + (subtotal * 0.15);
        }


    }
}
