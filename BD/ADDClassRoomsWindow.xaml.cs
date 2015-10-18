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
        ForWorkBD fwbd;
        int flag;
        int id;
        public ADDClassRoomsWindow(SqlConnection connection,int flag,int id)
        {
            InitializeComponent();
            this.connection = connection;
            fwbd = new ForWorkBD(connection);
            this.flag = flag;
            this.id = id;
        }     
        void ZapFormID()
        {
            string tmp="select Housing,Number where IDrooms="+id.ToString();
            SqlDataAdapter sqladapter = new SqlDataAdapter(tmp, connection);
            SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);
            tbHousing.Text=dt.Rows[0][0].ToString();
            tbNumber.Text = dt.Rows[0][0].ToString();
            chbClose.Visibility = Visibility.Hidden;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (flag == 0)
            {
                Grid grid1 = new Grid();
                fwbd.FillinfCheckBox(ref cb, ref cb1, ref mapping, "select * from Types", grid1);
                scr.Content = grid1;
            }
            else
            {
                Grid grid1 = new Grid();
                fwbd.FillinfCheckBox(ref cb, ref cb1, ref mapping, "select * from Types", grid1);
                scr.Content = grid1;


            }
           
        }
        
       void clear()
        {
           tbHousing.Text="1";
           tbNumber.Text = "1";
           Grid grid1 = new Grid();
           fwbd.FillinfCheckBox(ref cb, ref cb1, ref mapping, "select * from Types", grid1);
           scr.Content = grid1;
        }
        void addbt()
       {
          
            int k = fwbd.NumberID("select IDrooms from ClassRooms order by IDrooms Desc");
            SqlCommand command = new SqlCommand();
            command.Connection=connection;
            command.CommandText = "insert into ClassRooms (IDrooms,Housing,Number) Values(@IDrooms,@Housing,@Number)";
            command.Parameters.AddWithValue("@IDrooms", k);
            command.Parameters.AddWithValue("@Housing", tbHousing.Text);
            command.Parameters.AddWithValue("@Number" , tbNumber.Text);
            string tmp = "select IDrooms,Housing,Number from ClassRooms where Housing=" + tbHousing.Text + " AND  Number=" + tbNumber.Text;
            SqlDataAdapter sqladapter = new SqlDataAdapter(tmp, connection);
            SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);
            int ntmp = dt.Rows.Count;
            if(ntmp>0)
            {
                MessageBox.Show("Данная аудитория уже существует");
                return;
            }
            bool flg=true;
            for (int i = 0; i < cb.Count(); i++)
            {
                if(cb[i].IsChecked==true)
                {
                    flg = false;
                    i = cb.Count();
                }
            }
            if(flg)
            {
                MessageBox.Show("Не указаны типы аудиторий");
                return;
            }
            try
            {
              command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ERROR");
                return;
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
            if(chbClose.IsChecked ==true )
            {
                Close();
                return;
            }
            clear();
        
       }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0) addbt();
        }
     
    }
}
