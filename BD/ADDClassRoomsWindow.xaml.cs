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
        CheckBox[] cb;
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
            fr.FillinfCheckBox(ref cb, "select * from RoomTypes",grid1);           
            scr.Content = grid1;
        }
        string chboxreturn()//возврат значений отмеченных чекбоксов для бд
        {
            string s = "null";
            bool flag = false;
            for (int i = 0; i < cb.Count(); i++)
            {
                if(cb[i].IsChecked==true)
                {
                    if(flag)
                    {
                        s += "+"+cb[i].Content.ToString();
                    }
                    else
                    {
                        flag = true;
                        s = cb[i].Content.ToString();
                    }
                }
            }
            if (flag) s += "!";
            return s;
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string commandText = "",s;
            s=chboxreturn();
            if (s == "null")
            {
                MessageBox.Show("Необходимо выбрать тип аудитории");
                return;
            }
            commandText = "exec ADDClassRooms "+tbNumber.Text+","+tbHousing.Text+","+s;
            SqlCommand command = new SqlCommand(commandText, connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
           
        }
    }
}
