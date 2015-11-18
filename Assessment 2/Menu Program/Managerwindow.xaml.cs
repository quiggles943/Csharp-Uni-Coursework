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
        reader_writer rw = new reader_writer();
        
        public Managerwindow()
        {
            try
            {
                InitializeComponent();
                sitin = System.IO.Path.GetFullPath(sitin_orderpath);
                delivery = System.IO.Path.GetFullPath(delivery_orderpath);
                rw.sit = rw.readin_sitin;
                rw.deliver = rw.readin_delivery;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
            finally
            {
                statuslabel.Content = "Files loaded successfully";
                vegetarianbox.MaxLength = 1;
                start();
                fontsize();
            }
        }

        private void readinsettings()
        {
            int filelength = 0;
            using (StreamReader r = new StreamReader(p.settingfilepath))
            {
                while (r.ReadLine() != null) { filelength++; }
            }
            int i = 1;
            string[] file = System.IO.File.ReadAllLines(p.settingfilepath);
            int len = file.Length;
            while (i < (len))
            {
                string[] column = file[i].Split('\t');
                int j = 0;
                while (j < (column.Length))
                {
                    string buffer = column[j];
                    string buffer2 = buffer;
                    p.setting[i, j] = buffer2;
                    j++;
                }

                i++;
            }
            p.settinglength = filelength;
            filelength = 0;
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

        }

        public void fontsize()
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
            for( int i=1; i< rw.menulength; i++)
            {
                testlistbox.Items.Add(rw.menu[i, 0] + " - " + rw.count[i]);         //adds total amount of each item ordered to item box
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
                for (int i = 1; i <= (rw.serverlength); i++)
                {
                    item_selection.Items.Add(rw.server[i, 0]);
                }
                staffidlabel.Content = "Staff Id";
                vegetarianlabel.Visibility = Visibility.Hidden;
                vegetarianbox.Visibility = Visibility.Hidden;
            }

            if (edit_selection.SelectedIndex == 1)      //Drivers
            {
                for (int i = 1; i <= (rw.driverlength); i++)
                {
                    item_selection.Items.Add(rw.driver[i, 0]);
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
                for (int i = 1; i <= rw.menulength; i++)
                {
                    item_selection.Items.Add(rw.menu[i, 0]);
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
                    for (int i = 1; i <= rw.Serverlength; i++)
                    {
                        if (rw.server[i, 0] == item_selection.SelectedItem.ToString())
                        {
                            rw.server[i, 0] = namebox.Text;
                            rw.server[i, 1] = Int32.Parse(staffidbox.Text).ToString();
                        }
                    }                   
                    rw.Server = rw.server;

                }
                if (addrbtn.IsChecked == true)
                {
                    //rw.serverlength ++;
                    rw.server[rw.Serverlength, 0] = namebox.Text;
                    rw.server[rw.Serverlength, 1] = Int32.Parse(staffidbox.Text).ToString();
                    
                    
                    rw.Server = rw.server;
                }
                if (removerbtn.IsChecked == true)
                {
                    for (int i = 1; i <= rw.Serverlength; i++)
                    {
                        if (rw.server[i, 0] == item_selection.SelectedItem.ToString())
                        {
                            for (int j = i; j <= rw.Serverlength; j++)
                            {
                                rw.server[j, 0] = rw.server[j + 1, 0];
                                rw.server[j, 1] = rw.server[j + 1, 1];
                            }
                        }

                    }
                    rw.Serverlength = (rw.serverlength - 1);
                    rw.Server = rw.server;
                }
            }

            else if (edit_selection.SelectedIndex == 1)     //Drivers
            {

            }

            else if (edit_selection.SelectedIndex == 2)     //Menu Items
            {
                if (editrbtn.IsChecked == true)
                {
                    for (int i = 1; i <= rw.menulength; i++)
                    {
                        if (rw.menu[i, 0] == item_selection.SelectedItem.ToString())
                        {
                            rw.menu[i, 0] = namebox.Text;
                            rw.menu[i, 1] = Int32.Parse(staffidbox.Text).ToString();
                            rw.menu[rw.menulength, 2] = vegetarianbox.Text;
                        }
                    }
                    rw.Menu = rw.menu;

                }
                if (addrbtn.IsChecked == true)
                {
                    rw.menu[rw.menulength+1, 0] = namebox.Text;
                    rw.menu[rw.menulength+1, 1] = Int32.Parse(staffidbox.Text).ToString();
                    rw.menu[rw.menulength+1, 2] = vegetarianbox.Text;
                    rw.menulength = rw.menulength + 1;
                    rw.Menu = rw.menu;
                }
                if (removerbtn.IsChecked == true)
                {
                    for (int i = 1; i <= rw.menulength; i++)
                    {
                        if (rw.menu[i, 0] == item_selection.SelectedItem.ToString())
                        {
                            for (int j = i; j <= rw.menulength; j++)
                            {
                                rw.menu[j, 0] = rw.menu[j + 1, 0];
                                rw.menu[j, 1] = rw.menu[j + 1, 1];
                                rw.menu[j, 2] = rw.menu[j + 1, 2];
                            }
                        }

                    }
                    rw.menulength = rw.menulength - 1;
                    rw.Menu = rw.menu;
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
