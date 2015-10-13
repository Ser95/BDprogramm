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
            SqlConnectionStringBuilder strokapodkl = new SqlConnectionStringBuilder();
            strokapodkl.DataSource = "(LocalDB)\\v11.0";
            strokapodkl.AttachDBFilename = @"D:\документы\4институт\ЭС\расписание\BD\BD\TestDB.mdf";
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
            ADDClassRoomsWindow f = new ADDClassRoomsWindow(connection);
            f.Show();
        }
    }
}
