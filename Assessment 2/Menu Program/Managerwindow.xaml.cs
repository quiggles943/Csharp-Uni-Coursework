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
    /// Interaction logic for Managerwindow.xaml
    /// </summary>
    public partial class Managerwindow : Window
    {
        List<Order> sitin = new List<Order>();
        public Managerwindow(List<Order> sitinorders)
        {
            InitializeComponent();
            List<Order> sitin = sitinorders;
        }


        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sitinbtn_Click(object sender, RoutedEventArgs e)
        {
            int j = sitin.Count();
            for (int i = 0; i<= j; i++)
            {
                sitin.Find(x => x.Index == i);
            }
            /*foreach (Order i in sitin.)
            {
                testlistbox.Items.Add(i.Server);
            }*/
        }
    }
}
