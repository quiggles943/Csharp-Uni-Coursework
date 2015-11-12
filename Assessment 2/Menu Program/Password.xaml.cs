using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using System.Security;
using System.Runtime.InteropServices;

namespace Menu_Program
{
    /// <summary>
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : Window

    {
        int settinglength = 0;
        string settingpath = @"..\..\setting.txt";
        string settingfilepath;
        string[,] setting = new string[100, 2];
        public bool correct = false;
        

        public Password()
        {
            InitializeComponent();
            settingfilepath = System.IO.Path.GetFullPath(settingpath);
            readinpassword();
        }

       private void readinpassword()
        {
            int filelength = 0;
            using (StreamReader r = new StreamReader(settingfilepath))
            {
                while (r.ReadLine() != null) { filelength++; }
            }
            int i = 1;
            string[] file = System.IO.File.ReadAllLines(settingfilepath);
            int len = file.Length;
            while (i < (len))
            {
                string[] column = file[i].Split('\t');
                int j = 0;
                while (j < (column.Length))
                {
                    string buffer = column[j];
                    string buffer2 = buffer;
                    setting[i, j] = buffer2;
                    j++;
                }

                i++;
            }
            settinglength = filelength;
            filelength = 0;
        }

        private void writepassword()
        {
            string[] empty = new string[0];
            File.WriteAllLines(settingfilepath, empty);
            StreamWriter logfile = File.AppendText(settingfilepath);
            logfile.WriteLine("hello\tVariable");
            for (int i = 1; i <= settinglength+1; i++)
            {
                logfile.WriteLine(setting[i, 0] + "\t" + setting[i, 1]);
            }
            logfile.Close();
        }


        private void enterbtn_Click(object sender, RoutedEventArgs e)
        {
            string input = encryption.encrypt(passwordbox.Password);
            /*setting[1, 1] = input;
            writepassword();*/

            if(input == setting[1,1])
            {
                correct = true;
                this.Close();
            }
            else
                MessageBox.Show("Password incorrect", "error");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
