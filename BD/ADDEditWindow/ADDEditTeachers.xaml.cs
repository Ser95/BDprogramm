using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
namespace BD
{
    /// <summary>
    /// Логика взаимодействия для ADDEditTeachers.xaml
    /// </summary>
    public partial class ADDEditTeachers : Window
    {
        int flag;
       
        ForWorkBD fwbd;
        int id;
        public ADDEditTeachers(SqlConnection connection,int flag,int id)
        {
            InitializeComponent();
            this.flag = flag;
            this.id = id;
            fwbd = new ForWorkBD(connection);
        }
        bool Empty()
        {          
            if (tbFN.Text == "") return false;
            if (tbLN.Text == "") return false;
            if (tbSN.Text == "") return false;
            return true;
        }
        void EditLoad()
        {
            cbClose.Visibility = Visibility.Hidden;
            string sqlcom = "select IDTeachers,FName,LName,SName,Cafedra,Dolgn from Teachers where IDTeachers=" + id.ToString();
            SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcom,fwbd.connection);
            SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);
            lbID.Content = "ID=" + id.ToString();
            tbFN.Text = dt.Rows[0][1].ToString();
            tbLN.Text = dt.Rows[0][2].ToString();
            tbSN.Text = dt.Rows[0][3].ToString();
            tbCf.Text = dt.Rows[0][4].ToString();
            tbD.Text = dt.Rows[0][5].ToString();
            btnSave.Content = "Сохранить";
        }
        void ADDLoad()
        {

            id = fwbd.NumberID("select IDTeachers from Teachers order by IDTeachers Desc");
            if (id == -1)
            {
                MessageBox.Show("Error");
                Close();
                return;
            }
            lbID.Content ="ID="+ id.ToString();
            tbFN.Text="";
            tbLN.Text="";
            tbSN.Text = "";
            tbCf.Text = "";
            tbD.Text = "";
        }
        void ADDbt()
        {
            
            if(id==-1 )
            {
                MessageBox.Show("Error");
                Close();
                return;
            }
            if(!Empty())
            {
                MessageBox.Show("Необходимо заполнить поля ФИО");
                return;
            }
            SqlCommand command = new SqlCommand();
            command.Connection = fwbd.connection;
            string s = "insert into Teachers(IDTeachers,FName,LName,SName,Cafedra,Dolgn) values (@IDTeachers,@FName,@LName,@SName,@Cafedra,@Dolgn)";
            command.CommandText = s;
            command.Parameters.AddWithValue("@IDTeachers", id);
            command.Parameters.AddWithValue("@FName", tbFN.Text);
            command.Parameters.AddWithValue("@LName", tbLN.Text);
            command.Parameters.AddWithValue("@SName", tbSN.Text);
            command.Parameters.AddWithValue("@Cafedra", tbCf.Text);
            command.Parameters.AddWithValue("@Dolgn", tbD.Text);
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
            if (Empty())
            {
                MessageBox.Show("Необходимо заполнить поля ФИО");
                return;
            }
            string s = "Update Teachers set FName=@FName,LName=@FName,SName=@FName,Cafedra=@FName,Dolgn=@FName where IDTeachers=@IDTeachers";
            SqlCommand command = new SqlCommand();
            command.Connection = fwbd.connection;
            command.CommandText = s;
            command.Parameters.AddWithValue("@IDTeachers", id);
            command.Parameters.AddWithValue("@FName", tbFN.Text);
            command.Parameters.AddWithValue("@LName", tbLN.Text);
            command.Parameters.AddWithValue("@SName", tbSN.Text);
            command.Parameters.AddWithValue("@Cafedra", tbCf.Text);
            command.Parameters.AddWithValue("@Dolgn", tbD.Text);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ERROR");
                return;
            }
            MessageBox.Show("Запись Отредактирована");
            Close();

        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0) ADDbt();
            else Editbt();
        }     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (flag == 0) ADDLoad();
            else EditLoad();
        }
    }
}
