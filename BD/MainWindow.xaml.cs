using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            Zapusk();
        }
        void Zapusk()
        {
            string filename = System.Environment.CurrentDirectory + "\\bd4.mdf";
            SqlConnectionStringBuilder strokapodkl = new SqlConnectionStringBuilder();
            strokapodkl.DataSource = "(localdb)\\MSSQLLocalDB";
            strokapodkl.AttachDBFilename = filename;
            strokapodkl.IntegratedSecurity = true;
            strokapodkl.CurrentLanguage = "russian";
            connection = new SqlConnection(strokapodkl.ConnectionString);
            connection.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {          
            TableWindow f = new TableWindow(1,connection);
            f.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TableWindow f = new TableWindow(2, connection);
            f.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TableWindow f = new TableWindow(0, connection);
            f.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
          
            //ClassRoomsWindow f = new ClassRoomsWindow(connection);
            //f.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            BDrep r = new BDrep(connection);
            ClassRoomWindow f = new ClassRoomWindow(connection);
            f.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ClassesWindow f = new ClassesWindow(connection);
            f.ShowDialog();
        }
    }
}
