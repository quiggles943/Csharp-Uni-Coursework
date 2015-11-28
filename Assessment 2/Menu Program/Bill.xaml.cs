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
        public Bill(deliveryOrder d)
        {
            InitializeComponent();
            addresslabel.Visibility = Visibility.Visible;
            addresstxt.Visibility = Visibility.Visible;
            int total = d.items.Count;
            int i = 0;
            foreach (Menu m in d.items)
            {
                string item;
                if (d.readNote(i) != "none")
                    item = string.Join(" - ", m.Description, d.readNote(i));
                else
                    item = m.Description;
                billlist.Items.Add(item) ;
                i++;
            }
            subtotallabel.Content = Math.Round(d.Paid,2);
            deliv_chargelabel.Content = Math.Round((d.Paid * 0.15),2);
            totallabel.Content = Math.Round((d.Paid + (d.Paid * 0.15)),2);
            serverlabel.Content = d.Server;
            name_tablelabel.Content = "Name:";
            name_tablecontentlabel.Content = d.name;
            addresstxt.Content = d.Address;
        }
        public Bill(sitinOrder s, bool sitin)
        {
            InitializeComponent();
            deliv_chargelabel.Visibility = Visibility.Hidden;
            deliverychargelabel.Visibility = Visibility.Hidden;
            addresslabel.Visibility = Visibility.Hidden;
            addresstxt.Visibility = Visibility.Hidden;
            int total = s.items.Count;
            int i = 0;
            foreach (Menu m in s.items)
            {
                string item;
                if (s.readNote(i) != "none")
                    item = string.Join(" - ", m.Description, s.readNote(i));
                else
                    item = m.Description;
                billlist.Items.Add(item);
                i++;
            }
            subtotallabel.Content = Math.Round(s.Paid,2);
            deliv_chargelabel.Content = "0";
            totallabel.Content = Math.Round(s.Paid,2);
            serverlabel.Content = s.Server;
            name_tablelabel.Content = "Table:";
            name_tablecontentlabel.Content = s.Table;
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
