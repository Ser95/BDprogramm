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
    /// Логика взаимодействия для ADDEditSubgroupsWindow.xaml
    /// </summary>
    public partial class ADDEditSubgroupsWindow : Window
    {
        int flag;

        ForWorkBD fwbd;
        int id;
        public ADDEditSubgroupsWindow(SqlConnection connection, int flag, int id)
        {
            InitializeComponent();
            this.flag = flag;
            this.id = id;
            fwbd = new ForWorkBD(connection);
        }
        void EditLoad()
        {
            cbClose.Visibility = Visibility.Hidden;
            string sqlcom = "select IDGroups,NameGroup,NumberSubGroup from SubGroups where IDGroups=" + id.ToString();
            SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcom, fwbd.connection);
            SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);
            lbID.Content = "ID=" + id.ToString();            
            tbNameGroup.Text = dt.Rows[0][1].ToString();
            tbNumberSubGroup.Text = dt.Rows[0][2].ToString();           
            btnSave.Content = "Сохранить";
        }
        void ADDLoad()
        {
            id = fwbd.NumberID("select IDGroups from SubGroups order by IDGroups Desc");
            if (id == -1)
            {
                MessageBox.Show("Error");
                Close();
                return;
            }
            lbID.Content = "ID=" + id.ToString();
            tbNameGroup.Text = "";
            tbNumberSubGroup.Text = "";
           
        }
        void ADDbt()
        {
            if (id == -1)
            {
                MessageBox.Show("Error");
                return;
            }

            if (tbNumberSubGroup.Text == "" || tbNameGroup.Text == "")
            {
                MessageBox.Show("Необходимо указать группу и подгруппу");
                return;
            }
            SqlCommand command = new SqlCommand();
            command.Connection = fwbd.connection;
            string s = "insert into SubGroups(IDGroups,NameGroup,NumberSubGroup) values (@IDGroups,@NameGroup,@NumberSubGroup)";
            command.CommandText = s;
            command.Parameters.AddWithValue("@IDGroups", id);
            command.Parameters.AddWithValue("@NameGroup", tbNameGroup.Text);
            command.Parameters.AddWithValue("@NumberSubGroup", tbNumberSubGroup.Text);
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
            if (tbNumberSubGroup.Text == "" || tbNameGroup.Text == "")
            {
                MessageBox.Show("Необходимо указать группу и подгруппу");
                return;
            }
            SqlCommand command = new SqlCommand();
            command.Connection = fwbd.connection;
            string s = "Update SubGroups set NameGroup=@NameGroup,NumberSubGroup=@NumberSubGroup where IDGroups=@IDGroups";
            command.CommandText = s;
            command.Parameters.AddWithValue("@IDGroups", id);
            command.Parameters.AddWithValue("@NameGroup", tbNameGroup.Text);
            command.Parameters.AddWithValue("@NumberSubGroup", tbNumberSubGroup.Text);
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (flag == 0) ADDLoad();
            else EditLoad();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0) ADDbt();
            else Editbt();
        }
    }
}
