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
        int menulength;
        int sitinlength;
        int deliverylength;
        public Managerwindow(string delivery_filepath, string sitin_filepath, string[,] menuitems, int length)
        {
            InitializeComponent();
            sitin = sitin_filepath;
            delivery = delivery_filepath;
            menu = menuitems;
            menulength = length;
            readin_delivery();
            readin_sitin();
            
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

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sitinbtn_Click(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            for(int i = 1; i <= sitinlength; i++)
            {
                testlistbox.Items.Add(sit[i, 0]+" " +sit[i,1]+ " " +sit[i,2]);
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
    }
}
