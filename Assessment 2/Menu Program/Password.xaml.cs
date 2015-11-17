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
        public int settinglength = 0;
        string settingpath = @"..\..\setting.txt";
        public string settingfilepath;
        public string[,] setting = new string[100, 2];
        public bool correct = false;
        Setting s = new Setting();
        

        public Password()
        {
            InitializeComponent();
            passwordbox.Focus();
            settingfilepath = System.IO.Path.GetFullPath(settingpath);
            fontsize();
        }

        public void fontsize()
        {
            this.FontSize = s.Fontsize;
        }

        private void enterbtn_Click(object sender, RoutedEventArgs e)
        {
            string input = encryption.encrypt(passwordbox.Password);
            if(input == s.Password)
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

        private void keypress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                enterbtn_Click(sender, e);
            }
        }
        
    }
}
