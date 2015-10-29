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
        bool isloggedin = false;
        IDictionary<string, Menu> menuitems = new Dictionary<string, Menu>();
        public MainWindow()
        {
            InitializeComponent();
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);  
            readin();
        }

        private void readin()
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
                menuitems[menu[i, 0]] = new Menu(menu[i, 0],theanswer ,Int32.Parse(menu[i, 1])); 
                foodlistbox.Items.Add(menu[i, 0]);
                i++;
            }
            filelength = 0;
            //read in server text file
            using (StreamReader r = new StreamReader(serverfilepath))
            {
                while (r.ReadLine() != null) { filelength++; }
            }
            i = 1;
            file = System.IO.File.ReadAllLines(serverfilepath);
            len = file.Length;
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
                isloggedin = true;
                foodlistbox.IsEnabled = true;
                serverlist.IsEnabled = false;
                serverstatusbox.Content = serverlist.SelectedItem;
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
            
            orderlistbox.Items.Add(foodlistbox.SelectedItem);
            string buffer = foodlistbox.SelectedItem.ToString();
            price.Content =  menuitems[buffer].Price;
        }


    }
}
