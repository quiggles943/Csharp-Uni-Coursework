using System;
using System.Collections.Generic;
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
    /// Interaction logic for changePassword.xaml
    /// </summary>
    public partial class changePassword : Window
    {
        bool correct = false;
        Password p = new Password();
        Setting setting = new Setting();
        public changePassword()
        {
            InitializeComponent();
            old1.Focus();
            passwordmatch.Content = "passwords do not match";
            fontsize();
        }

        public void fontsize()
        {
            this.FontSize = setting.Fontsize;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (encryption.encrypt(old1.Password) == setting.Password)
            {
                if (newPassword.Password == new2.Password && newPassword.Password != "")
                {
                    passwordmatch.Content = "Passwords match";
                    correct = true;
                }
                if (correct)
                {
                    setting.Password = encryption.encrypt(newPassword.Password);
                    this.Close();
                }
            }
            else
            { 
                MessageBox.Show("Old password does not match stored password", "Password Incorrect", MessageBoxButton.OK); 
            }
        }

        private void textchanged(object sender, RoutedEventArgs e)
        {
            if (newPassword.Password == new2.Password)
            {
                if (newPassword.Password == new2.Password && newPassword.Password != "")
                {
                    passwordmatch.Content = "Passwords match";
                    correct = true;
                }
                else
                {
                    passwordmatch.Content = "Passwords do not match";
                    correct = false;
                }
            }
        }

        private void oldkeypress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                newPassword.Focus();
            }
        }

        private void new1keypress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                new2.Focus();
            }
        }

        private void newPasswordKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_Click(sender, e);
            }
        }
    }
}
