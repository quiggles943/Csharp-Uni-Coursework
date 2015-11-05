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
    /// Interaction logic for Bill.xaml
    /// </summary>
    public partial class Bill : Window
    {
        public Bill(Order d)
        {
            InitializeComponent();
            //testtxt2.Content = d.Price;
            int total = d.items.Count;
            foreach (Menu m in d.items)
            {
                billlist.Items.Add(m.Description) ;
            }
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void billlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
