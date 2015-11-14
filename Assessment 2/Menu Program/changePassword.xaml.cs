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
        }

        private void old2_TextChanged(object sender, RoutedEventArgs e)
        {
            if (old1.Password == old2.Password)
            {
                passwordmatch.Content = "passwords match";
                correct = true;
            }
            else
                passwordmatch.Content = "passwords do not match";
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (old1.Password == old2.Password && newPassword.Password != "")
            {
                passwordmatch.Content = "passwords match";
                correct = true;
            }
            if (correct)
            {
                setting.Password = encryption.encrypt(newPassword.Password);
            }
        }
    }
}
