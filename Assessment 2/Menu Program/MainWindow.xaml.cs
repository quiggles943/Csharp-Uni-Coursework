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


    }
}
