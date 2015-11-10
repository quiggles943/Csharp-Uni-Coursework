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
            int total = d.items.Count;
            foreach (Menu m in d.items)
            {
                billlist.Items.Add(m.Description) ;
            }
            subtotallabel.Content = Math.Round(d.Paid,2);
            deliv_chargelabel.Content = Math.Round((d.Paid * 0.15),2);
            totallabel.Content = Math.Round((d.Paid + (d.Paid * 0.15)),2);

        }
        public Bill(Order s, bool sitin)
        {
            InitializeComponent();
            deliv_chargelabel.Visibility = Visibility.Hidden;
            deliverychargelabel.Visibility = Visibility.Hidden;
            int total = s.items.Count;
            foreach (Menu m in s.items)
            {
                billlist.Items.Add(m.Description);
            }
            subtotallabel.Content = Math.Round(s.Paid,2);
            deliv_chargelabel.Content = "0";
            totallabel.Content = Math.Round(s.Paid,2);

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
