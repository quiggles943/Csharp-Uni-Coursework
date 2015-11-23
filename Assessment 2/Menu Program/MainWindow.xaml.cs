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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Menu_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int filelength = 0;
        public string[,] menu = new string[100,5];
        public string[,] server = new string[100, 2];
        public string[,] driver = new string[100, 3];
        string menupath = @"..\..\menu.ini";
        string serverpath = @"..\..\server.ini";
        string sitin_orderpath = @"..\..\sitin_orderlog.ini";
        string delivery_orderpath = @"..\..\delivery_orderlog.ini";
        string driverpath = @"..\..\driver.ini";
        string menufilepath;
        string serverfilepath;
        string driverfilepath;
        string sitin_order_filepath;
        string delivery_order_filepath;
        string items;
        int menulength;
        int serverlength;
        int driverlength;
        bool sitin;
        double subtotal;
        int table;
        int itemsordered = 0;
        List<Menu> menuitems = new List<Menu>();
        //Order o = new Order();
        deliveryOrder d = new deliveryOrder();
        SitinOrder s = new SitinOrder();
        Setting setting = new Setting();
        public List<Order> sitinorders = new List<Order>();
        List<Menu> orderedItems = new List<Menu>();
        bool detailsadded = false;
        //int denotes number of tables in restaraunt
        int tablenos = 20;
        reader_writer rw = new reader_writer();
        
        public MainWindow()
        {

            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            driverfilepath = System.IO.Path.GetFullPath(driverpath);
            sitin_order_filepath = System.IO.Path.GetFullPath(sitin_orderpath);
            delivery_order_filepath = System.IO.Path.GetFullPath(delivery_orderpath);
            InitializeComponent();
            tablebox.Visibility = Visibility.Hidden;
            tabletxt.Visibility = Visibility.Hidden;
            addtablebtn.Visibility = Visibility.Hidden;
            tablelabel.Visibility = Visibility.Hidden;
            tablebox.Visibility = Visibility.Hidden;
            destinationlabel.Visibility = Visibility.Hidden;
            detailsbtn.Visibility = Visibility.Hidden;
            nametxtbox.Visibility = Visibility.Hidden;
            addresstxtbox.Visibility = Visibility.Hidden;
            namelabel.Visibility = Visibility.Hidden;
            addresslabel.Visibility = Visibility.Hidden;
            driverbox.Visibility = Visibility.Hidden;
            orderlistbox.IsEnabled = false;
            sitinradbtn.IsEnabled = false;
            takeawayradbtn.IsEnabled = false;
            logoutbtn.IsEnabled = false;
            readin();
            //readinmenuitems();
            //readinservers();
            //menureadin();
            //readindrivers();
            fontsize();
            
        }

        public void readin()
        {
            rw.MenuRead();
            rw.ServerRead();
            rw.DriverRead();
            foreach (var item in rw.menuitems)
            {
                foodlistbox.Items.Add(item.Description);
            }
            
            foreach (var item in rw.servers)
            {
                serverlist.Items.Add(item.name);
            }
            
            foreach (var item in rw.drivers)
            {
                driverbox.Items.Add(item.name);
            }
        }

        public void readinmenuitems()
        {
            
        }
        public void readinservers()
        {
            //rw.ServerRead();
            foreach (var item in rw.servers)
            {
                serverlist.Items.Add(item.name);
            }
        }

        public void readindrivers()
        {
            
        }

        public void fontsize()
        {
            this.FontSize = setting.Fontsize;
        }

        private void writetofile(string server, int table, double paid)     //writes to sit in order log 
        {
            string items = "";
            for(int i =0; i<= itemsordered-1; i++)
            {
            if(i == 0)
                items = s.items[i].Description;
            else
                items = string.Join("\t", items, s.items[i].Description);
            }
            if (File.Exists(sitin_order_filepath))
            {
                DateTime time = DateTime.Now;             // Use current time.
                using (StreamWriter logfile = File.AppendText(sitin_order_filepath))
                {

                    logfile.WriteLine(time.ToString("g") + "\t" + server + "\t" + table + "\t£" + paid + "\t" + items);
                }
            }
            else
            {
                using (StreamWriter logfile = File.CreateText(sitin_order_filepath))
                {
                    DateTime time = DateTime.Now;
                    logfile.WriteLine("Server\tTable\tAmountpaid");
                    logfile.WriteLine(time.ToString("g") + "\t" + server + "\t" + table + "\t£" + paid + "\t" + items);
                }
            }
            
        }
        private void writetofile(string server, string driver, string name, double paid)        //writes to delivery order log 
        {
            string items = "";
            for (int i = 0; i <= itemsordered - 1; i++)
            {
                if (i == 0)
                    items = d.items[i].Description;
                else
                    items = string.Join("\t", items, d.items[i].Description);
            }
            if (File.Exists(delivery_order_filepath))
            {

                using (StreamWriter logfile = File.AppendText(delivery_order_filepath))
                {
                    DateTime time = DateTime.Now;
                    logfile.WriteLine(time.ToString("g") + "\t" + server + "\t" + driver + "\t" + name + "\t£" + paid + "\t" + items);
                }
            }
            else
            {
                using (StreamWriter logfile = File.CreateText(delivery_order_filepath))
                {
                    DateTime time = DateTime.Now;
                    logfile.WriteLine("Server\tDriver\tCust Name\tAmountpaid");
                    logfile.WriteLine(time.ToString("g") + "\t" + server + "\t" + driver + "\t" + name + "\t£" + paid + "\t" + items);
                }
            }

        }

        private void serverlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void logonbtn_Click(object sender, RoutedEventArgs e)
        {
            if (serverlist.SelectedIndex != -1)
            {
                serverlist.IsEnabled = false;
                serverstatusbox.Content = serverlist.SelectedItem;
                sitinradbtn.IsEnabled = true;
                takeawayradbtn.IsEnabled = true;
                logoutbtn.IsEnabled = true;
                logonbtn.IsEnabled = false;
                selectbtn.IsEnabled = true;
                statuslabel.Content = "Logged on";
            }
            else
                return;

        }

        private void foodlistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (foodlistbox.SelectedIndex == -1)
            {
                orderlistbox.IsEnabled = false;
            }
            else
                orderlistbox.IsEnabled = true;

        }

        private void addtobtn_Click(object sender, RoutedEventArgs e)
        {
            if (foodlistbox.SelectedIndex == -1)
                return;
            double buffer = 0;
            if (sitin)
            {
                buffer = rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                s.Dishes(rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()));
            }
            else
            {
                buffer = rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                d.Dishes(rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()));
            }
            orderlistbox.Items.Add(foodlistbox.SelectedItem);
            subtotal  = subtotal + Math.Round(buffer,2);
            subtotallabel.Content = subtotal;
            //orderedItems.Add(rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()));
            itemsordered++;
            statuslabel.Content = "Item added";
        }

        private void sitinradbtn_Checked(object sender, RoutedEventArgs e)
        {
            sitin = true;   
        }

        private void takeawayradbtn_Checked(object sender, RoutedEventArgs e)
        {
            sitin = false;
            tablebox.Visibility = Visibility.Hidden;
            tabletxt.Visibility = Visibility.Hidden;
            tabletxt.Content = "";
            destinationlabel.Content = "";
            
            
        }

        private void selectbtn_Click(object sender, RoutedEventArgs e)
        {

            if (sitin)
            {
                addtablebtn.Visibility = Visibility.Visible;
                tablelabel.Visibility = Visibility.Visible;
                tablebox.Visibility = Visibility.Visible;
                nametxtbox.Visibility = Visibility.Hidden;
                nametxtbox.Text = "";
                addresstxtbox.Visibility = Visibility.Hidden;
                addresstxtbox.Text = "";
                namelabel.Visibility = Visibility.Hidden;
                addresslabel.Visibility = Visibility.Hidden;
                statuslabel.Content = "Sit-in selected";
                tabletxt.Visibility = Visibility.Visible;
                destinationlabel.Visibility = Visibility.Visible;
                destinationlabel.Content = "Table:";
                
                
            }
            else
            {
                detailsbtn.Visibility = Visibility.Visible;
                nametxtbox.Visibility = Visibility.Visible;
                addresstxtbox.Visibility = Visibility.Visible;
                namelabel.Visibility = Visibility.Visible;
                addresslabel.Visibility = Visibility.Visible;
                driverbox.Visibility = Visibility.Visible;
                nametxtbox.IsEnabled = true;
                addresstxtbox.IsEnabled = true;
                statuslabel.Content = "Delivery selected";

            }
            destinationlabel.Visibility = Visibility.Visible;
            addtobtn.IsEnabled = true;
            sitinradbtn.IsEnabled = false;
            takeawayradbtn.IsEnabled = false;
            
        }

        private void addtablebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(tablebox.Text) < 1 || Int32.Parse(tablebox.Text) > tablenos)
                    throw new ArgumentException("Table number invalid");
                table = Int32.Parse(tablebox.Text);
                tabletxt.Content = table;
                statuslabel.Content = "Table selected";
                foodlistbox.IsEnabled = true;
                addtablebtn.IsEnabled = false;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "error");
            }
        }

        internal void clearbtn_Click(object sender, RoutedEventArgs e)
        {
            tablebox.Clear();
            subtotal = 0;
            subtotallabel.Content = "0.00";
            table = 0;
            tabletxt.Content = "";
            tablelabel.Visibility = Visibility.Hidden;
            addtablebtn.Visibility = Visibility.Hidden;
            tablelabel.Visibility = Visibility.Hidden;
            tablebox.Visibility = Visibility.Hidden;
            destinationlabel.Visibility = Visibility.Hidden;
            addtablebtn.IsEnabled = true;
            sitinradbtn.IsEnabled = true;
            takeawayradbtn.IsEnabled = true;
            //foodlistbox.Items.Clear();
            foodlistbox.IsEnabled = false;
            orderlistbox.Items.Clear();
            nametxtbox.Visibility = Visibility.Hidden;
            nametxtbox.Text = "";
            addresstxtbox.Visibility = Visibility.Hidden;
            addresstxtbox.Text = "";
            namelabel.Visibility = Visibility.Hidden;
            addresslabel.Visibility = Visibility.Hidden;
            nametxtbox.IsEnabled = true;
            addresstxtbox.IsEnabled = true;
            detailsbtn.Visibility = Visibility.Hidden;
            detailsadded = false;
            d.Clear();
            s.Clear();
            driverbox.Visibility = Visibility.Hidden;
            driverbox.SelectedIndex = -1;
            statuslabel.Content = "Order cleared";

        }

        private void totalbtn_Click(object sender, RoutedEventArgs e)
        {
            orderlistbox.IsEnabled = false;
            foodlistbox.IsEnabled = false;
            addtobtn.IsEnabled = false;
            statuslabel.Content = "Subtotal generated";

        }

        private void detailsbtn_Click(object sender, RoutedEventArgs e)
        {
            if (detailsadded)
                return;
            try
            {
                if(driverbox.SelectedIndex == -1)
                    throw new ArgumentException("No driver selected");
                d.Driver = driverbox.SelectedItem.ToString();
                d.Name = nametxtbox.Text;
                d.Address = addresstxtbox.Text;
                foodlistbox.IsEnabled = true;
                addtablebtn.Visibility = Visibility.Hidden;
                tablelabel.Visibility = Visibility.Hidden;
                tablebox.Visibility = Visibility.Hidden;
                d.Name = nametxtbox.Text;
                d.Address = addresstxtbox.Text;
                destinationlabel.Content = "Name";
                tabletxt.Content = nametxtbox.Text;
                nametxtbox.IsEnabled = false;
                addresstxtbox.IsEnabled = false;
                detailsadded = true;
                statuslabel.Content = "Details added";
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "error");
            }

        }

        private void nametxtbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void billbtn_Click(object sender, RoutedEventArgs e)
        {
            if(itemsordered == 0)
            {
                statuslabel.Content = "No items in order";
                return;
            }
            totalbtn_Click(sender, e);
            if (sitin)
            {
                //writetofile(serverlist.SelectedItem.ToString(), table, subtotal, items);
                writetofile(serverlist.SelectedItem.ToString(), table, subtotal);
                s.Paid = subtotal;
                Window Bill = new Bill(s, sitin);
                Bill.ShowDialog();
                orderedItems.Clear();
                logoutbtn_Click(sender, e);
            }
            else
            {
                string driver = driverbox.SelectedItem.ToString();
                d.Paid = subtotal;
                writetofile(serverlist.SelectedItem.ToString(), driver,  nametxtbox.Text, subtotal);
                Window Bill = new Bill(d);
                Bill.ShowDialog();
                orderedItems.Clear();
                logoutbtn_Click(sender, e);
            }
            statuslabel.Content = "Bill printed";
            

        }

        private void logoutbtn_Click(object sender, RoutedEventArgs e)
        {
            clearbtn_Click(sender, e);
            selectbtn.IsEnabled = false;
            sitinradbtn.IsEnabled = false;
            takeawayradbtn.IsEnabled = false;
            serverlist.IsEnabled = true;
            logoutbtn.IsEnabled = false;
            logonbtn.IsEnabled = true;
            statuslabel.Content = "Logged out";
        }

        public void managerbtn_Click(object sender, RoutedEventArgs e)
        {
            Password password = new Password();
            password.ShowDialog();
            if (password.correct)
            {
                Managerwindow manager = new Managerwindow(rw);
                manager.ShowDialog();
                //fontsize();
                
            }
            else
                MessageBox.Show("Password incorrect", "error");
            fontsize();
            readinmenuitems();
            readindrivers();
            readinservers();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Password password = new Password();
            password.ShowDialog();
        }

        private void closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void foodlistbox_doubleclick(object sender, MouseButtonEventArgs e)
        {
            addtobtn_Click(sender, e);
        }

        private void removeitembtn_Click(object sender, RoutedEventArgs e)
        {
            if (orderlistbox.SelectedIndex == -1)
                return;
            double buffer = 0;
            if (sitin)
            {
                buffer = rw.menuitems.Find(x => x.Description == orderlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                s.removeDish(rw.menuitems.Find(x => x.Description == orderlistbox.SelectedItem.ToString()));
            }
            else
            {
                buffer = rw.menuitems.Find(x => x.Description == orderlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                d.removeDish(rw.menuitems.Find(x => x.Description == orderlistbox.SelectedItem.ToString()));
            }
            orderlistbox.Items.Remove(orderlistbox.SelectedItem);
            subtotal = subtotal - Math.Round(buffer, 2);
            subtotallabel.Content = subtotal;
            //orderedItems.Remove(rw.menuitems.Find(x => x.Description == orderlistbox.SelectedItem.ToString()));
            itemsordered--;
            statuslabel.Content = "Item Removed";
        }

        private void Order_doubleClick(object sender, MouseButtonEventArgs e)
        {
            removeitembtn_Click(sender, e);
        }
    }
}
