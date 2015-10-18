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
    /// Логика взаимодействия для ClassesWindow.xaml
    /// </summary>
    public partial class ClassesWindow : Window
    {
        ForWorkBD fwbd;
        public ClassesWindow(SqlConnection connection)
        {
            InitializeComponent();
            fwbd = new ForWorkBD(connection);
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            ADDEDITClassesWindow f = new ADDEDITClassesWindow(fwbd.connection, 0, 0);
            f.ShowDialog();
            ZapGrid();
        }
        void ZapGrid()
        {
            dgViewdata.ItemsSource = null;
            ///получение списка всех пар
            SqlDataAdapter sqladapter = new SqlDataAdapter("select ID,Name,Count from Classes order by ID asc", fwbd.connection);
            SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            
            int n = dt.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                //получение все id типов аудиторий
                string tmp = dt.Rows[i][0].ToString();
                sqladapter = new SqlDataAdapter("select IDTypes  from ClassesTypes where ID=" + dt.Rows[i][0].ToString(), fwbd.connection);
                sqlcmd = new SqlCommandBuilder(sqladapter);
                DataTable dtidtype = new DataTable();
                sqladapter.Fill(dtidtype);
                string type = "";
                for (int j = 0; j < dtidtype.Rows.Count; j++)
                {
                    sqladapter = new SqlDataAdapter("select Description  from Types where IDTypes=" + dtidtype.Rows[j][0].ToString(), fwbd.connection);
                    sqlcmd = new SqlCommandBuilder(sqladapter);
                    DataTable dttype = new DataTable();
                    sqladapter.Fill(dttype);
                    type += dttype.Rows[0][0].ToString();                 
                    type += "\n";
                }
                dt.Rows[i][3] = type;
            }
            for (int i = 0; i < n; i++)
            {
                //получение все id преподов
                string tmp = dt.Rows[i][0].ToString();
                sqladapter = new SqlDataAdapter("select IDTeachers  from ClassesTeachers where ID=" + dt.Rows[i][0].ToString(), fwbd.connection);
                sqlcmd = new SqlCommandBuilder(sqladapter);
                DataTable dtidtype = new DataTable();
                sqladapter.Fill(dtidtype);
                string type = "";
                for (int j = 0; j < dtidtype.Rows.Count; j++)
                {
                    sqladapter = new SqlDataAdapter("select FName,LName,SName,Cafedra  from Teachers where IDTeachers=" + dtidtype.Rows[j][0].ToString(), fwbd.connection);
                    sqlcmd = new SqlCommandBuilder(sqladapter);
                    DataTable dttype = new DataTable();
                    sqladapter.Fill(dttype);
                    type += dttype.Rows[0][0].ToString() +" " +dttype.Rows[0][1].ToString() +" "+ dttype.Rows[0][2].ToString() +"  "+ dttype.Rows[0][3].ToString();
                    type += "\n";
                }
                dt.Rows[i][4] = type;
            }
            for (int i = 0; i < n; i++)
            {
                //получение все id групп
                string tmp = dt.Rows[i][0].ToString();
                sqladapter = new SqlDataAdapter("select IDGroups  from ClassesSubGroups where ID=" + dt.Rows[i][0].ToString(), fwbd.connection);
                sqlcmd = new SqlCommandBuilder(sqladapter);
                DataTable dtidtype = new DataTable();
                sqladapter.Fill(dtidtype);
                string type = "";
                for (int j = 0; j < dtidtype.Rows.Count; j++)
                {
                    sqladapter = new SqlDataAdapter("select NameGroup,NumberSubGroup  from SubGroups where IDGroups=" + dtidtype.Rows[j][0].ToString(), fwbd.connection);
                    sqlcmd = new SqlCommandBuilder(sqladapter);
                    DataTable dttype = new DataTable();
                    sqladapter.Fill(dttype);
                    type += dttype.Rows[0][0].ToString() + " " + dttype.Rows[0][1].ToString();
                    type += "\n";
                }
                dt.Rows[i][5] = type;
            }


            dgViewdata.ItemsSource = dt.DefaultView;
            dgViewdata.Columns[0].Header = "ID";
            dgViewdata.Columns[1].Header = "Название";
            dgViewdata.Columns[2].Header = "Количество";
            dgViewdata.Columns[4].Header = "Преподаватель";
            dgViewdata.Columns[5].Header = "Подгруппы";
            dgViewdata.Columns[3].Header = "Типы аудитории";




        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ZapGrid();
        }



    }
}
