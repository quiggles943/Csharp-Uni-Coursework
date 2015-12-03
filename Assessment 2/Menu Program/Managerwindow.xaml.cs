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
using System.Windows.Threading;

namespace Menu_Program
{
    /// <summary>
    /// Interaction logic for Managerwindow.xaml
    /// </summary>
    public partial class Managerwindow : Window
    {
        string sitin;
        string delivery;
        string server;
        string sitin_orderpath = @"..\..\sitin_orderlog.ini";
        string delivery_orderpath = @"..\..\delivery_orderlog.ini";
        Password p = new Password();
        Setting s = new Setting();
        reader_writer rw;

        public Managerwindow(reader_writer r, string s)
        {
            Time();
            rw = r;
            server = s;
            InitializeComponent();
            sitin = System.IO.Path.GetFullPath(sitin_orderpath);
            delivery = System.IO.Path.GetFullPath(delivery_orderpath);
            rw.readin_sitin();
            rw.readin_delivery();
            rw.IdListRead();
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
            serverstatusbox.Content = server;
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
            DateTime dt;
            DateTime Today = DateTime.Today;
            testlistbox.Items.Clear();
            testlistbox.Items.Add("Sit In Orders");
            testlistbox.Items.Add("Date/Time\tServer\tTable\tAmount");
            for (int i = 1; i <= rw.sitinlength-1; i++)
            {
                string server;
                if (rw.servers.Find(x => x.ID == Int32.Parse(rw.sit[i, 1])).Equals(null))
                    server = "N/A";
                else
                server = rw.servers.Find(x => x.ID == Int32.Parse(rw.sit[i, 1])).name;
                dt = Convert.ToDateTime(rw.sit[i, 0]);
                if (orderdatebox.SelectedIndex == 0)
                {
                    testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + server + "\t" + rw.sit[i, 2] + "\t" + rw.sit[i, 3]);
                }
                if(orderdatebox.SelectedIndex == 1)
                {
                    if (dt.Date == Today)
                    {
                        testlistbox.Items.Add(dt.ToShortTimeString() + "\t\t" + server + "\t" + rw.sit[i, 2] + "\t" + rw.sit[i, 3]);
                    }
                }
                if (orderdatebox.SelectedIndex == 2)
                {
                    if (dt.Date <= DateTime.Today && dt >= Today.AddDays(-7))
                    {
                        testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + server + "\t" + rw.sit[i, 2] + "\t" + rw.sit[i, 3]);
                    }
                }
                if (orderdatebox.SelectedIndex == 3)
                {
                    if (dt.Date <= DateTime.Today && dt >= Today.AddDays(-28))
                    {
                        testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + server + "\t" + rw.sit[i, 2] + "\t" + rw.sit[i, 3]);
                    }
                }

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
            DateTime dt;
            DateTime Today = DateTime.Today;
            testlistbox.Items.Clear();
            testlistbox.Items.Add("Delivery Orders");
            testlistbox.Items.Add("Date/Time\tServer\tDriver\tCustomer\tAmount");
            for (int i = 1; i <= rw.deliverylength - 1; i++)
            {
                dt = Convert.ToDateTime(rw.deliver[i, 0]);
                if (orderdatebox.SelectedIndex == 0)
                {
                    testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x=> x.ID == Int32.Parse(rw.deliver[i,1])).name + "\t" + rw.deliver[i, 2] + "\t" + rw.deliver[i, 3] + "\t\t" + rw.deliver[i, 4]/* + "\t" + rw.deliver[i, 4]*/);
                }
                if (orderdatebox.SelectedIndex == 1)
                {
                    if (dt.Date == Today)
                    {
                        testlistbox.Items.Add(dt.ToShortTimeString() + "\t\t" + rw.servers.Find(x=> x.ID == Int32.Parse(rw.deliver[i,1])).name + "\t" + rw.deliver[i, 2] + "\t" + rw.deliver[i, 3] + "\t\t" + rw.deliver[i, 4]/* + "\t" + rw.deliver[i, 4]*/);
                    }
                }
                if (orderdatebox.SelectedIndex == 2)
                {
                    if (dt.Date <= DateTime.Today && dt >= Today.AddDays(-7))
                    {
                        testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x=> x.ID == Int32.Parse(rw.deliver[i,1])).name + "\t" + rw.deliver[i, 2] + "\t" + rw.deliver[i, 3] + "\t\t" + rw.deliver[i, 4] + "\t" /*+ rw.deliver[i, 4]*/);
                    }
                }
                if (orderdatebox.SelectedIndex == 3)
                {
                    if (dt.Date <= DateTime.Today && dt >= Today.AddDays(-28))
                    {
                        testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x=> x.ID == Int32.Parse(rw.deliver[i,1])).name + "\t" + rw.deliver[i, 2] + "\t" + rw.deliver[i, 3] + "\t\t" + rw.deliver[i, 4] + "\t" /*+ rw.deliver[i, 4]*/);
                    }
                }

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
             DateTime dt;
            DateTime Today = DateTime.Today;
            testlistbox.Items.Clear();
            testlistbox.Items.Add("Sit In Orders for " + serverbox.SelectedItem.ToString());
            testlistbox.Items.Add("Date/Time\tServer\tTable\tAmount");
                for (int j = 1; j <= rw.sitinlength - 1; j++)
                {
                    dt = Convert.ToDateTime(rw.sit[j, 0]);
                    if (rw.servers.Find(x => x.ID == Int32.Parse(rw.sit[j, 1])).name == serverbox.SelectedItem.ToString())
                    {
                        if (orderdatebox.SelectedIndex == 0)
                        {
                            testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x => x.ID == Int32.Parse(rw.sit[j, 1])).name + "\t" + rw.sit[j, 2] + "\t" + rw.sit[j, 3]);
                        }
                        if (orderdatebox.SelectedIndex == 1)
                        {
                            if (dt.Date == Today)
                            {
                                testlistbox.Items.Add(dt.ToShortTimeString() + "\t\t" + rw.servers.Find(x => x.ID == Int32.Parse(rw.sit[j, 1])).name + "\t" + rw.sit[j, 2] + "\t" + rw.sit[j, 3]);
                            }
                        }
                        if (orderdatebox.SelectedIndex == 2)
                        {
                            if (dt.Date <= DateTime.Today && dt >= Today.AddDays(-7))
                            {
                                testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x => x.ID == Int32.Parse(rw.sit[j, 1])).name + "\t" + rw.sit[j, 2] + "\t" + rw.sit[j, 3]);
                            }
                        }
                        if (orderdatebox.SelectedIndex == 3)
                        {
                            if (dt.Date <= DateTime.Today && dt >= Today.AddDays(-28))
                            {
                                testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x => x.ID == Int32.Parse(rw.sit[j, 1])).name + "\t" + rw.sit[j, 2] + "\t" + rw.sit[j, 3]);
                            }
                        }
                    }

                }
                string dash = "-";
                for (int k = 0; k <= rw.longsitin+32; k++)
                    dash = string.Concat(dash, "-");
                testlistbox.Items.Add(dash);
                testlistbox.Items.Add("Delivery Orders for " + serverbox.SelectedItem.ToString());
                testlistbox.Items.Add("Date/Time\tServer\tDriver\tCustomer\tAmount");
                    for (int i = 1; i <= rw.deliverylength - 1; i++)
                    {
                        dt = Convert.ToDateTime(rw.deliver[i, 0]);
                        if (rw.servers.Find(x=> x.ID == Int32.Parse(rw.deliver[i,1])).name == serverbox.SelectedItem.ToString())
                        {
                            if (orderdatebox.SelectedIndex == 0)
                            {
                                testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x=> x.ID == Int32.Parse(rw.deliver[i,1])).name + "\t" + rw.deliver[i, 2] + "\t" + rw.deliver[i, 3] + "\t\t" + rw.deliver[i, 4]/* + "\t" + rw.deliver[i, 4]*/);
                            }
                            if (orderdatebox.SelectedIndex == 1)
                            {
                                if (dt.Date == Today)
                                {
                                    testlistbox.Items.Add(dt.ToShortTimeString() + "\t\t" + rw.servers.Find(x => x.ID == Int32.Parse(rw.deliver[i,1])).name + "\t" + rw.deliver[i, 2] + "\t" + rw.deliver[i, 3] + "\t\t" + rw.deliver[i, 4]/* + "\t" + rw.deliver[i, 4]*/);
                                }
                            }
                            if (orderdatebox.SelectedIndex == 2)
                            {
                                if (dt.Date <= DateTime.Today && dt >= Today.AddDays(-7))
                                {
                                    testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x=> x.ID == Int32.Parse(rw.deliver[i,1])).name + "\t" + rw.deliver[i, 2] + "\t" + rw.deliver[i, 3] + "\t\t" + rw.deliver[i, 4] + "\t" /*+ rw.deliver[i, 4]*/);
                                }
                            }
                            if (orderdatebox.SelectedIndex == 3)
                            {
                                if (dt.Date <= DateTime.Today && dt >= Today.AddDays(-28))
                                {
                                    testlistbox.Items.Add(dt.ToString("dd HH:mm") + "\t\t" + rw.servers.Find(x=> x.ID == Int32.Parse(rw.deliver[i,1])).name + "\t" + rw.deliver[i, 2] + "\t" + rw.deliver[i, 3] + "\t\t" + rw.deliver[i, 4] + "\t" /*+ rw.deliver[i, 4]*/);
                                }
                            }
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
                vegetarianlabel.Content = "             Car Reg:";
                vegetarianbox.MaxLength = 8;
                vegetarianbox.Width = 68;
            }
            if(edit_selection.SelectedIndex == 2)       //Menu Items
            {
                vegetarianbox.Clear();
                vegetarianbox.IsEnabled = false;
                foreach (var item in rw.menuitems)
                {
                    item_selection.Items.Add(item.Description);
                }
            
                staffidlabel.Content = "  Price £";
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
            staffidbox.IsEnabled = true;
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
            addbtn.Content = ("Add " + edit_selection.SelectionBoxItem.ToString());
        }

        private void editrbtn_Checked(object sender, RoutedEventArgs e)
        {
            if (edit_selection.SelectionBoxItem.ToString() == "Menu Items")
                staffidbox.IsEnabled = true;
            else
            staffidbox.IsEnabled = false;
            staffidbox.Clear();
            autofill();
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
            addbtn.Content = ("Edit " + edit_selection.SelectionBoxItem.ToString());
        }

        private void removerbtn_Checked(object sender, RoutedEventArgs e)
        {
            staffidbox.IsEnabled = true;
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
            addbtn.Content = ("Remove " + edit_selection.SelectionBoxItem.ToString());
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (addrbtn.IsChecked == true)
                {
                    foreach (var value in rw.used_ids)
                    {
                        if (Int32.Parse(staffidbox.Text) == value)
                        {
                            throw new ArgumentException("Id is already in use");
                        }
                    }
                }
                if (edit_selection.SelectedIndex == 0)      //Servers
                {
                    if (editrbtn.IsChecked == true)
                    {
                        var edititem = rw.servers.FindIndex(x => x.name == item_selection.SelectedItem.ToString());
                        rw.servers[edititem].name = namebox.Text;
                        rw.servers[edititem].ID = Int32.Parse(staffidbox.Text);
                        rw.ServerWrite();       //writes changes to file

                    }
                    if (addrbtn.IsChecked == true)
                    {
                        rw.servers.Add(new Server(namebox.Text, Int32.Parse(staffidbox.Text)));
                        rw.used_ids.Add(Int32.Parse(staffidbox.Text));
                        rw.IdListWrite();
                        rw.ServerWrite();       //writes changes to file
                    }
                    if (removerbtn.IsChecked == true)
                    {
                        var removeitem = rw.servers.Single(x => x.name == item_selection.SelectedItem.ToString());
                        rw.servers.Remove(removeitem);
                        rw.ServerWrite();       //writes changes to file
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
                        rw.DriverWrite();       //writes changes to file

                    }
                    if (addrbtn.IsChecked == true)
                    {
                        rw.drivers.Add(new Driver(namebox.Text, Int32.Parse(staffidbox.Text), vegetarianbox.Text));
                        rw.used_ids.Add(Int32.Parse(staffidbox.Text));
                        rw.IdListWrite();
                        rw.DriverWrite();       //writes changes to file
                    }
                    if (removerbtn.IsChecked == true)
                    {
                        var removeitem = rw.drivers.Single(x => x.name == item_selection.SelectedItem.ToString());
                        rw.drivers.Remove(removeitem);
                        rw.DriverWrite();       //writes changes to file
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
                            rw.MenuWrite();     //writes changes to file
                        }
                        catch (Exception excep)
                        {
                            MessageBox.Show(excep.Message, "error");
                        }

                    }
                    if (addrbtn.IsChecked == true)
                    {
                        rw.menuitems.Add(new Menu(namebox.Text, veg, Int32.Parse(staffidbox.Text)));
                        rw.MenuWrite();     //writes changes to file
                    }
                    if (removerbtn.IsChecked == true)
                    {
                        var removeitem = rw.menuitems.Single(x => x.Description == item_selection.SelectedItem.ToString());
                        rw.menuitems.Remove(removeitem);
                        rw.MenuWrite();     //writes changes to file
                    }
                }
                start();
            }
            catch(Exception excp)
            {
                MessageBox.Show(excp.Message, "Error");
            }
        }

        private void item_selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            namebox.Clear();
            staffidbox.Clear();
            vegetarianbox.Clear();
            autofill();

            if (edit_selection.SelectedIndex == 0)     //Servers
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Time()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1.0)
            };
            timer.Tick += (o, e) =>
            {
                time.Content = DateTime.Now.ToString("HH:mm");
            };
            timer.Start();
        }

        private void testlistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void autofill()
        {
            if(item_selection.SelectedIndex == -1)
            {
                namebox.Clear();
                return;
            }
            if (edit_selection.SelectedIndex == 0)
            {
                if (edit_selection.SelectionBoxItem.ToString() == "Servers")
                {
                    var edititem = rw.servers.FindIndex(x => x.name == item_selection.SelectedItem.ToString());
                    namebox.Text = rw.servers[edititem].name;
                    staffidbox.Text = rw.servers[edititem].ID.ToString();
                }
            }

            if (edit_selection.SelectedIndex == 1)
            {
                if (edit_selection.SelectionBoxItem.ToString() == "Drivers")
                {
                    var edititem = rw.drivers.FindIndex(x => x.name == item_selection.SelectedItem.ToString());
                    namebox.Text = rw.drivers[edititem].name;
                    staffidbox.Text = rw.drivers[edititem].ID.ToString();
                }
            }
        }
    }
}
