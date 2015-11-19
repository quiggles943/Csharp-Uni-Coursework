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
        string sitin_orderpath = @"..\..\sitin_orderlog.ini";
        string delivery_orderpath = @"..\..\delivery_orderlog.ini";
        Password p = new Password();
        Setting s = new Setting();
        reader_writer rw;
        
        public Managerwindow(reader_writer r)
        {
            /*try
            {*/
                rw = r;
                InitializeComponent();
                sitin = System.IO.Path.GetFullPath(sitin_orderpath);
                delivery = System.IO.Path.GetFullPath(delivery_orderpath);
                rw.readin_sitin();
                rw.readin_delivery();
            //}
            /*catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }*/
            /*finally
            {*/
                if (rw.sitinfound && rw.deliveryfound)
                    statuslabel.Content = "Files loaded successfully";
                if (rw.sitinfound == false)
                    statuslabel.Content = "Sit-in log file missing";
                if (rw.deliveryfound == false)
                    statuslabel.Content = "Delivery log file missing";
                if (rw.sitinfound == false && rw.deliveryfound == false)
                    statuslabel.Content = "Log files not found";
                vegetarianbox.MaxLength = 1;
                start();
                fontsize();
            //}
        }

        private void start()        //sets visibility for items on page
        {
            namelabel.Visibility = Visibility.Hidden;
            namebox.Clear();
            namebox.Visibility = Visibility.Hidden;
            staffidlabel.Visibility = Visibility.Hidden;
            staffidbox.Clear();
            staffidbox.Visibility = Visibility.Hidden;
            vegetarianlabel.Visibility = Visibility.Hidden;
            vegetarianbox.Clear();
            vegetarianbox.Visibility = Visibility.Hidden;
            addrbtn.Visibility = Visibility.Hidden;
            editrbtn.Visibility = Visibility.Hidden;
            removerbtn.Visibility = Visibility.Hidden;
            item_selection.Visibility = Visibility.Hidden;
            edit_selection.SelectedIndex = -1;
            server_combobox();

        }

        public void server_combobox()       //updates the server selection combo box
        {
            serverbox.Items.Clear();
            foreach(var item in rw.servers)
            {
                serverbox.Items.Add(item.name);
            }
        }

        public void fontsize()      //reads in and sets font size
        {
            this.FontSize = s.Fontsize;
        }

        private void closebtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void sitinbtn_Click(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            testlistbox.Items.Add("Server\tTable\tAmount");
            for (int i = 1; i <= rw.sitinlength; i++)
            {
                testlistbox.Items.Add(rw.sit[i, 0] + "\t" + rw.sit[i, 1] + "\t" + rw.sit[i, 2]);       //adds sit in orders to item box
            }
        }


        private void total_itemsbtn_Click(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            testlistbox.Items.Add("Menu Item     Amount");
            foreach (var item in rw.menuitems)
            {
                testlistbox.Items.Add(item.Description + " - " + item.Count);       //adds total amount of each item ordered to item box
            }
        }

        private void deliverybtn_Click_1(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();
            testlistbox.Items.Add("Server\tDriver\tCustomer\tAmount");
            for (int i = 1; i <= rw.deliverylength; i++)
            {
                testlistbox.Items.Add(rw.deliver[i, 0] + "\t" +rw.deliver[i,1] +"\t"+ rw.deliver[i, 2] + "\t\t" + rw.deliver[i, 3]);       //adds delivery orders to item box
            }
        }

        private void serverbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (serverbox.SelectedIndex == -1)
            {
                serverbtn.IsEnabled = false;
                serverbtn.Content = "Please select server";
            }

            else
            {
                serverbtn.IsEnabled = true;
                serverbtn.Content = "Show orders for " + serverbox.SelectedItem.ToString();
            }
        }

        private void serverbtn_Click(object sender, RoutedEventArgs e)
        {
            testlistbox.Items.Clear();

            for (int i = 1; i <= rw.sitinlength; i++)
            {
                if (serverbox.SelectedItem.ToString() == rw.sit[i, 0])
                {
                    testlistbox.Items.Add(rw.sit[i, 1] + " " + rw.sit[i, 2]);
                }
            }
                for (int j = 1; j <= rw.deliverylength; j++)
                {
                    if (serverbox.SelectedItem.ToString() == rw.deliver[j, 0])
                {
                    testlistbox.Items.Add(rw.deliver[j, 2] + " " + rw.deliver[j, 3]);
                }

            }
        }

        private void edit_selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item_selection.Items.Clear();
            if (edit_selection.SelectedIndex != -1)
            {
                addrbtn.Visibility = Visibility.Visible;
                editrbtn.Visibility = Visibility.Visible;
                removerbtn.Visibility = Visibility.Visible;
            }
            if (edit_selection.SelectedIndex == 0)      //Servers
            {
                foreach (var item in rw.servers)
                {
                    item_selection.Items.Add(item.name);
                }
                staffidlabel.Content = "Staff Id";
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
            }

            if (edit_selection.SelectedIndex == 1)      //Drivers
            {
                foreach (var item in rw.drivers)
                {
                    item_selection.Items.Add(item.name);
                }
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
                vegetarianlabel.Content = "               Car Reg:";
                vegetarianbox.MaxLength = 8;
                vegetarianbox.Width = 64;
            }
            if(edit_selection.SelectedIndex == 2)       //Menu Items
            {
                vegetarianbox.Clear();
                foreach (var item in rw.menuitems)
                {
                    item_selection.Items.Add(item.Description);
                }
            
                staffidlabel.Content = "Price £";
                vegetarianbox.MaxLength = 1;
                vegetarianlabel.Content = "Vegetarian (Y/N):";
                vegetarianbox.Width = 16;
            }
            else
            {
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
                staffidlabel.Content = "Staff Id:";
            }
            addrbtn.IsChecked = false;
            editrbtn.IsChecked = false;
            removerbtn.IsChecked = false;
            vegetarianbox.Clear();
        }

        private void addrbtn_Checked(object sender, RoutedEventArgs e)
        {
            namelabel.Visibility = Visibility.Visible;
            namebox.Visibility = Visibility.Visible;
            staffidlabel.Visibility = Visibility.Visible;
            staffidbox.Visibility = Visibility.Visible;
            item_selection.Visibility = Visibility.Hidden;
            if (edit_selection.SelectedIndex == 0)
            {
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
            }
            else
            {
                vegetarianlabel.Visibility = Visibility.Visible;
                vegetarianbox.Visibility = Visibility.Visible;
            }
        }

        private void editrbtn_Checked(object sender, RoutedEventArgs e)
        {
            namebox.Text = "";
            staffidbox.Text = "";
            item_selection.Visibility = Visibility.Visible;
            namelabel.Visibility = Visibility.Visible;
            namebox.Visibility = Visibility.Visible;
            staffidlabel.Visibility = Visibility.Visible;
            staffidbox.Visibility = Visibility.Visible;
            if (edit_selection.SelectedIndex == 0)
            {
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
            }
            else
            {
                vegetarianlabel.Visibility = Visibility.Visible;
                vegetarianbox.Visibility = Visibility.Visible;
            }
        }

        private void removerbtn_Checked(object sender, RoutedEventArgs e)
        {
            namelabel.Visibility = Visibility.Hidden;
            namebox.Visibility = Visibility.Hidden;
            staffidlabel.Visibility = Visibility.Hidden;
            staffidbox.Visibility = Visibility.Hidden;
            namebox.Text = "";
            staffidbox.Text = "";
            item_selection.Visibility = Visibility.Visible;
            if (edit_selection.SelectedIndex == 0)
            {
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
            }
            else
            {
                vegetarianlabel.Visibility = Visibility.Visible;
                vegetarianbox.Visibility = Visibility.Visible;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (edit_selection.SelectedIndex == 0)      //Servers
            {
                if (editrbtn.IsChecked == true)
                {
                    var edititem = rw.servers.FindIndex(x => x.name == item_selection.SelectedItem.ToString());
                    rw.servers[edititem].name = namebox.Text;
                    rw.servers[edititem].ID = Int32.Parse(staffidbox.Text);
                    rw.ServerWrite();

                }
                if (addrbtn.IsChecked == true)
                {
                    rw.servers.Add(new Server(namebox.Text, Int32.Parse(staffidbox.Text)));
                    rw.ServerWrite();
                }
                if (removerbtn.IsChecked == true)
                {
                    var removeitem = rw.servers.Single(x => x.name == item_selection.SelectedItem.ToString());
                    rw.servers.Remove(removeitem);
                    rw.ServerWrite();
                }
            }

            else if (edit_selection.SelectedIndex == 1)     //Drivers
            {
                if (editrbtn.IsChecked == true)
                {
                    var edititem = rw.drivers.FindIndex(x => x.name == item_selection.SelectedItem.ToString());
                    rw.drivers[edititem].name = namebox.Text;
                    rw.drivers[edititem].ID = Int32.Parse(staffidbox.Text);
                    rw.drivers[edititem].name = vegetarianbox.Text;
                    rw.DriverWrite();

                }
                if (addrbtn.IsChecked == true)
                {
                    rw.drivers.Add(new Driver(namebox.Text, Int32.Parse(staffidbox.Text), vegetarianbox.Text));
                    rw.DriverWrite();
                }
                if (removerbtn.IsChecked == true)
                {
                    var removeitem = rw.drivers.Single(x => x.name == item_selection.SelectedItem.ToString());
                    rw.drivers.Remove(removeitem);
                    rw.DriverWrite();
                }
            }

            else if (edit_selection.SelectedIndex == 2)     //Menu Items
            {
                bool veg = false; ;
                switch (vegetarianbox.Text)
                {
                    case "Y": veg = true; break;
                    case "N": veg = false; break;
                    default: throw new ArgumentException("Vegetarian input not accepted");

                }
                if (editrbtn.IsChecked == true)
                {
                    
                    try
                    {
                        var edititem = rw.servers.FindIndex(x => x.name == item_selection.SelectedItem.ToString());
                        rw.menuitems[edititem].Description = namebox.Text;
                        rw.menuitems[edititem].Price = Int32.Parse(staffidbox.Text);
                        rw.menuitems[edititem].Vegetarian = veg;
                        rw.MenuWrite();
                    }
                    catch (Exception excep)
                    {
                        MessageBox.Show(excep.Message, "error");
                    }

                }
                if (addrbtn.IsChecked == true)
                {
                    rw.menuitems.Add(new Menu(namebox.Text, veg, Int32.Parse(staffidbox.Text)));
                    rw.MenuWrite();
                }
                if (removerbtn.IsChecked == true)
                {
                    var removeitem = rw.menuitems.Single(x => x.Description == item_selection.SelectedItem.ToString());
                    rw.menuitems.Remove(removeitem);
                    rw.MenuWrite();
                }
            }
            start();

        }

        private void item_selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (edit_selection.SelectedIndex == 2)     //Menu Items
            {
                vegetarianlabel.Visibility = Visibility.Visible;
                vegetarianbox.Visibility = Visibility.Visible;
            }
            else
            {
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
            }
        }

        private void passwordbtn_Click(object sender, RoutedEventArgs e)
        {
            changePassword password = new changePassword();
            password.ShowDialog();
        }


        private void size12_Click(object sender, RoutedEventArgs e)
        {
            s.Fontsize = 13;
            //p.setting[2, 1] = s.Fontsize.ToString();
            this.FontSize = s.Fontsize;
        }

        private void size14_Click(object sender, RoutedEventArgs e)
        {
            s.Fontsize = 15;
            //p.setting[2, 1] = s.Fontsize.ToString();
            this.FontSize = s.Fontsize;
        }

    }
}
