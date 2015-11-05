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
        string[,] sit = new string[100, 20];
        public Managerwindow(string delivery_filepath, string sitin_filepath)
        {
            InitializeComponent();
            sitin = sitin_filepath;
            delivery = delivery_filepath;
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
                    j++;
                }
                testlistbox.Items.Add(sit[i, 0]);
                i++;
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sitinbtn_Click(object sender, RoutedEventArgs e)
        {
            readin_sitin();
            /*foreach (Order i in sitin.)
            {
                testlistbox.Items.Add(i.Server);
            }*/
        }
    }
}
