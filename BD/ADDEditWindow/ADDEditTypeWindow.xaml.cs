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
    /// Логика взаимодействия для ADDEditTypeWindow.xaml
    /// </summary>
    public partial class ADDEditTypeWindow : Window
    {
        int flag;

        ForWorkBD fwbd;
        int id;
        public ADDEditTypeWindow(SqlConnection connection, int flag, int id)
        {
            InitializeComponent();
            this.flag = flag;
            this.id = id;
            fwbd = new ForWorkBD(connection);
        }
        void ADDLoad()
        {
            id = fwbd.NumberID("select IDTypes from Types order by IDTypes Desc");
            if (id == -1)
            {
                MessageBox.Show("Error");
                Close();
                return;
            }
            lbID.Content = "ID=" + id.ToString();
            tbDescription.Text = "";
           
        }
        void EditLoad()
        {
            cbClose.Visibility = Visibility.Hidden;
            string sqlcom = "select IDTypes,Description from Types where IDTypes=" + id.ToString();
            SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcom, fwbd.connection);
            SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);
            lbID.Content = "ID=" + id.ToString();
            tbDescription.Text = dt.Rows[0][1].ToString();           
            btnSave.Content = "Сохранить";
        }
        void ADDbt()
        {
            if (id == -1)
            {
                MessageBox.Show("Error");
                return;
            }
            if (tbDescription.Text=="")
            {
                MessageBox.Show("Необходимо указать тип аудитории");
                return;
            }
            SqlCommand command = new SqlCommand();
            command.Connection = fwbd.connection;
            string s = "insert into Types(IDTypes,Description) values (@IDTypes,@Description)";
            command.CommandText = s;
            command.Parameters.AddWithValue("@IDTypes", id);
            command.Parameters.AddWithValue("@Description", tbDescription.Text);           
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ERROR");
                return;
            }
            MessageBox.Show("Запись добавлена");
            if (cbClose.IsChecked == true)
            {
                Close();
            }
            ADDLoad();
        }
        void Editbt()
        {
            if (tbDescription.Text == "")
            {
                MessageBox.Show("Необходимо указать тип аудитории");
                return;
            }
            SqlCommand command = new SqlCommand();
            command.Connection = fwbd.connection;
            string s = "Update Types set Description=@Description where IDTypes=@IDTypes";
            command.CommandText = s;
            command.Parameters.AddWithValue("@IDTypes", id);
            command.Parameters.AddWithValue("@Description", tbDescription.Text);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ERROR");
                return;
            }
            MessageBox.Show("Отредактировано");
            Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0) ADDbt();
            else Editbt();
        }

     

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (flag == 0) ADDLoad();
            else EditLoad();
        }
    }
}
