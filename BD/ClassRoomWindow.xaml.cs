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
    /// Логика взаимодействия для ClassRoomWindow.xaml
    /// </summary>
    public partial class ClassRoomWindow : Window
    {
        ForWorkBD fwbd;
        public ClassRoomWindow(SqlConnection connection)
        {
            InitializeComponent();
            fwbd = new ForWorkBD(connection);
        }
        void FillingDG()
        {
            dg.ItemsSource = null;
            ///получение списка всех аудиторий
            SqlDataAdapter sqladapter = new SqlDataAdapter("select IDrooms,Housing,Number from ClassRooms order by IDrooms asc", fwbd.connection);
            SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);
            dt.Columns.Add();

            int n = dt.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                //получение все id типов аудиторий
                string tmp = dt.Rows[i][0].ToString();
                sqladapter = new SqlDataAdapter("select IDTypes,flag  from ClassRoomsTypes where IDrooms="+dt.Rows[i][0].ToString(), fwbd.connection);
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
                    if (dtidtype.Rows[j][1].ToString() == "1") type += "(Не желательно)";
                    type += "\n";
                }
                dt.Rows[i][3] = type;
            }
            dg.ItemsSource = dt.DefaultView;
            dg.Columns[0].Header = "ID";
            dg.Columns[1].Header = "Корпус";
            dg.Columns[2].Header = "Номер аудитории";
            dg.Columns[3].Header = "Типы аудитории";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillingDG();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выделить запись");
                return;
            }
            if (dg.SelectedCells.Count >4)
            {
                MessageBox.Show("Необходимо выделить одну запись");
                return;
            }
            DataRowView rowView = dg.SelectedValue as DataRowView;
           string  s = "delete from ClassRooms where IDrooms=" + rowView[0].ToString();
            if (fwbd.ExecCommand(s))
            {
                MessageBox.Show("Удаление прошло успешно");
                FillingDG();
            }
            else
            {
                MessageBox.Show("не удалось");
                return;
            }

        }

        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            ADDClassRoomsWindow f = new ADDClassRoomsWindow(fwbd.connection,0,4);
            f.ShowDialog();
            FillingDG();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выделить запись");
                return;
            }
            if (dg.SelectedCells.Count >4)
            {
                MessageBox.Show("Необходимо выделить одну запись");
                return;
            }
            MessageBox.Show("Пока не работает:(");
            return;
            DataRowView rowView = dg.SelectedValue as DataRowView;
            ADDClassRoomsWindow f = new ADDClassRoomsWindow(fwbd.connection, 1,Convert.ToInt32(rowView[0].ToString()));
            f.ShowDialog();
            FillingDG();
        }
    }
}
