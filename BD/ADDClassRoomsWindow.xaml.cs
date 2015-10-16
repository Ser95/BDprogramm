using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для ADDClassRoomsWindow.xaml
    /// </summary>
    public partial class ADDClassRoomsWindow : Window
    {
        int countcheckbox;
        SqlConnection connection;
        CheckBox[] cb,cb1;
        int[] mapping;
        public ADDClassRoomsWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
         

        }
        //проблема по извлечению checkbox
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid1 = new Grid();
            ForWorkBD fr = new ForWorkBD(connection);
            fr.FillinfCheckBox(ref cb,ref cb1,ref mapping, "select * from Types",grid1);           
            scr.Content = grid1;
        }
        
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ForWorkBD fwbd = new ForWorkBD(connection);
            int k = fwbd.NumberID("select IDrooms from ClassRooms order by IDrooms Desc");
            SqlCommand command = new SqlCommand();
            command.Connection=connection;
            command.CommandText = "insert into ClassRooms (IDrooms,Housing,Number) Values(@IDrooms,@Housing,@Number)";
            command.Parameters.AddWithValue("@IDrooms", k);
            command.Parameters.AddWithValue("@Housing", tbHousing.Text);
            command.Parameters.AddWithValue("@Number", tbNumber.Text);
            try
            {
              command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
          
            for (int i = 0; i < cb.Count(); i++)
            {
                string s = "insert into ClassRoomsTypes(IDrooms,IDTypes,Flag) Values(@IDrooms,@IDTypes,@flag)";
                command = new SqlCommand(s);
                command.Connection = connection;
                if(cb[i].IsChecked==true)
                {
                    command.Parameters.AddWithValue("@IDTypes", mapping[i]);
                    command.Parameters.AddWithValue("@IDrooms", k);
                    if (cb1[i].IsChecked == true) command.Parameters.AddWithValue("@flag", 1);
                    else command.Parameters.AddWithValue("@flag", 0); 
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("ERROR");
                        return;
                    }
                   
                }
            }
            MessageBox.Show("Добавлено");
        }
     
    }
}
