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
using System.Windows.Threading;
namespace Menu_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[,] menu = new string[100, 5];
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
        bool sitin;
        int table;
        int itemsordered = 0;
        List<Menu> menuitems = new List<Menu>();
        deliveryOrder d = new deliveryOrder();
        sitinOrder s = new sitinOrder();
        Setting setting = new Setting();
        public List<Order> sitinorders = new List<Order>();
        List<Menu> orderedItems = new List<Menu>();
        bool detailsadded = false;

        //int denotes number of tables in restaraunt
        int tablenos = 20;

        reader_writer rw = new reader_writer();

        public MainWindow()
        {
            Time();
            menufilepath = System.IO.Path.GetFullPath(menupath);
            serverfilepath = System.IO.Path.GetFullPath(serverpath);
            driverfilepath = System.IO.Path.GetFullPath(driverpath);
            sitin_order_filepath = System.IO.Path.GetFullPath(sitin_orderpath);
            delivery_order_filepath = System.IO.Path.GetFullPath(delivery_orderpath);
            InitializeComponent();

            //sets initial visibility 
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
            driverlabel.Visibility = Visibility.Hidden;

            orderlistbox.IsEnabled = false;
            sitinradbtn.IsEnabled = false;
            takeawayradbtn.IsEnabled = false;
            logoutbtn.IsEnabled = false;
            notesbox.IsEnabled = false;
            notebtn.IsEnabled = false;
            removeitembtn.IsEnabled = false;
            readin();
            fontsize();

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
        public void readin()
        {
            foodlistbox.Items.Clear();
            serverlist.Items.Clear();
            driverbox.Items.Clear();
            rw.MenuRead();
            rw.ServerRead();
            rw.DriverRead();
            foreach (var item in rw.menuitems)
            {
                foodlistbox.Items.Add(item.Description);        //reads in each item to the food list box
            }

            foreach (var item in rw.servers)
            {
                serverlist.Items.Add(item.Name);        //reads in each server to the serverlist
            }

            foreach (var item in rw.drivers)
            {
                driverbox.Items.Add(item.Name);     //reads in each driver to the driver list
            }
        }

        public void fontsize()
        {
            this.FontSize = setting.Fontsize;       //sets the fontsize
        }

        private void writetofile(int server, int table, double paid)     //writes to sit in order log 
        {
            string items = "";
            //creates the string of items for the order log
            for (int i = 0; i <= itemsordered - 1; i++)
            {
                if (i == 0)
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
        private void writetofile(int server, string driver, string name, double paid)        //writes to delivery order log 
        {
            string items = "";
            //creates the string of items for the order log
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
                //sets the required enabled constraints
                serverlist.IsEnabled = false;
                serverstatusbox.Content = serverlist.SelectedItem;
                sitinradbtn.IsEnabled = true;
                takeawayradbtn.IsEnabled = true;
                logoutbtn.IsEnabled = true;
                logonbtn.IsEnabled = false;
                selectbtn.IsEnabled = true;
                statuslabel.Content = "Logged on";
                managerbtn.IsEnabled = true;
                selectbtn.IsEnabled = true;
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
                orderlistbox.IsEnabled = true;  //enables the orderlistbox

        }

        private void addtobtn_Click(object sender, RoutedEventArgs e)
        {
            if (foodlistbox.SelectedIndex == -1)
                return;     //ensures no error if no item selected
            double buffer = 0;
            if (sitin)      //adds item to sitin order
            {
                buffer = rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                s.Dishes(rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()));
                s.Note("none");
                subtotallabel.Content = Math.Round((s.total()/100),2);
            }
            else           //adds item to delivery order
            {
                buffer = rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                d.Dishes(rw.menuitems.Find(x => x.Description == foodlistbox.SelectedItem.ToString()));
                d.Note("none");
                subtotallabel.Content = Math.Round((d.total() / 100), 2);
            }
            orderlistbox.Items.Add(foodlistbox.SelectedItem);
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
            if (sitinradbtn.IsChecked == false && takeawayradbtn.IsChecked == false)
            {
                return;
            }
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
                s.Server = serverlist.SelectedItem.ToString();


            }
            else
            {
                detailsbtn.Visibility = Visibility.Visible;
                nametxtbox.Visibility = Visibility.Visible;
                addresstxtbox.Visibility = Visibility.Visible;
                namelabel.Visibility = Visibility.Visible;
                addresslabel.Visibility = Visibility.Visible;
                driverbox.Visibility = Visibility.Visible;
                driverlabel.Visibility = Visibility.Visible;
                nametxtbox.IsEnabled = true;
                addresstxtbox.IsEnabled = true;
                statuslabel.Content = "Delivery selected";
                d.Server = serverlist.SelectedItem.ToString();

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
                s.Table = table;
                notesbox.IsEnabled = true;
                notebtn.IsEnabled = true;
                removeitembtn.IsEnabled = true;
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "error");
            }
        }

        internal void clearbtn_Click(object sender, RoutedEventArgs e)
        {
            tablebox.Clear();
            //subtotal = 0;
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
            driverlabel.Visibility = Visibility.Hidden;
            driverbox.SelectedIndex = -1;
            notesbox.Clear();
            notesbox.IsEnabled = false;
            notebtn.IsEnabled = false;
            removeitembtn.IsEnabled = false;
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
                if (driverbox.SelectedIndex == -1)
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
                tabletxt.Content = d.Name;
                nametxtbox.IsEnabled = false;
                addresstxtbox.IsEnabled = false;
                detailsadded = true;
                notesbox.IsEnabled = true;
                notebtn.IsEnabled = true;
                removeitembtn.IsEnabled = true;
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
            if (itemsordered == 0)
            {
                statuslabel.Content = "No items in order";
                return;
            }
            totalbtn_Click(sender, e);
            if (sitin)
            {
                writetofile(rw.servers[serverlist.SelectedIndex].ID, table, Math.Round((s.total() / 100), 2));
                s.Paid = Math.Round((s.total() / 100), 2);
                Window Bill = new Bill(s, sitin);
                Bill.ShowDialog();
                orderedItems.Clear();
                logoutbtn_Click(sender, e);
            }
            else
            {
                string driver = driverbox.SelectedItem.ToString();
                d.Paid = Math.Round((d.total() / 100), 2);
                writetofile(rw.servers[serverlist.SelectedIndex].ID, driver, nametxtbox.Text, Math.Round((d.total() / 100), 2));
                Window Bill = new Bill(d);
                Bill.ShowDialog();
                orderedItems.Clear();
                logoutbtn_Click(sender, e);
            }
            statuslabel.Content = "Bill printed";
            itemsordered = 0;

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
            managerbtn.IsEnabled = false;
            sitinradbtn.IsChecked = false;
            takeawayradbtn.IsChecked = false;
            serverlist.SelectedIndex = -1;
            selectbtn.IsEnabled = false;
            serverstatusbox.Content = "";
        }

        public void managerbtn_Click(object sender, RoutedEventArgs e)
        {
            Password password = new Password();
            password.ShowDialog();
            if (password.correct)
            {
                Managerwindow manager = new Managerwindow(rw, serverlist.SelectedItem.ToString());
                manager.ShowDialog();

            }
            else
                MessageBox.Show("Password incorrect", "error");
            fontsize();
            readin();
            logoutbtn_Click(sender, e);
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
                s.items.RemoveAt(orderlistbox.SelectedIndex);
                s.removeNote(orderlistbox.SelectedIndex);
                subtotallabel.Content = Math.Round((s.total() / 100), 2);
            }
            else
            {
                buffer = rw.menuitems.Find(x => x.Description == orderlistbox.SelectedItem.ToString()).Price;
                buffer = (buffer / 100);
                d.items.RemoveAt(orderlistbox.SelectedIndex);
                d.removeNote(orderlistbox.SelectedIndex);
                subtotallabel.Content = Math.Round((d.total() / 100), 2);
            }
            orderlistbox.Items.RemoveAt(orderlistbox.SelectedIndex);
            itemsordered--;
            statuslabel.Content = "Item Removed";
        }

        private void Order_doubleClick(object sender, MouseButtonEventArgs e)
        {
            removeitembtn_Click(sender, e);
        }

        private void notebtn_Click(object sender, RoutedEventArgs e)
        {
            if (orderlistbox.SelectedIndex == -1)
                return;
            if (sitin)
                s.addNote(orderlistbox.SelectedIndex, notesbox.Text);
            else
                d.addNote(orderlistbox.SelectedIndex, notesbox.Text);
        }

        private void orderlistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orderlistbox.SelectedIndex == -1)
                return;
            if (sitin)
                notesbox.Text = s.readNote(orderlistbox.SelectedIndex);
            else
                notesbox.Text = d.readNote(orderlistbox.SelectedIndex);
        }
    }
}
