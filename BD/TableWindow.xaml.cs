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
    /// Логика взаимодействия для TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        int flag;        
        ForWorkBD fwbd;
        string CommandSelect;
        string Name;
        void ZapuskForm()
        {
            if (flag == 0)
            {
                CommandSelect = "select * from Types";
                Name = "Типы аудиторий";
                Title = Name;
            }
            else if(flag==1)
            {                
                CommandSelect = "select * from Teachers";
                Name = "Преподаватели";
                Title = Name;
            }
            else if (flag == 2)
            {
                CommandSelect = "select * from SubGroups";
                Name = "Подгруппы";
                Title = Name;                
            }


        }
        private void HiddenColumns()
        {
            int i = 0;
            int Visiblecolumn=0;
            if (flag == 0)
            {
                Visiblecolumn = 2;
                dgViewData.Columns[0].Header ="IDдля себя";
                dgViewData.Columns[1].Header = "Тип аудитории";
               
            }
            else if (flag == 1)
            {
                Visiblecolumn = 2;
                dgViewData.Columns[0].Header = "Номер преподавателя";
                dgViewData.Columns[1].Header = "ФИО преподавателя";
            }
            else
            {
                Visiblecolumn = 3;
                dgViewData.Columns[0].Header = "ID";
                dgViewData.Columns[1].Header = "Название группы";
                dgViewData.Columns[2].Header = "Номер подгруппы";
            }
            foreach (DataGridColumn column in dgViewData.Columns)
            {
                if (i < Visiblecolumn) column.Visibility = Visibility.Visible;
                else column.Visibility = Visibility.Collapsed;
                i++;
            }
        }

        public TableWindow(int flag, SqlConnection connection)
        {
            InitializeComponent();
            fwbd = new ForWorkBD(connection);
            this.flag = flag;
            ZapuskForm();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fwbd.FillingDataGrid(dgViewData, CommandSelect,Name);
            HiddenColumns();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (dgViewData.SelectedIndex == -1)
            {
                MessageBox.Show("Необходимо выделить запись");
                return;
            }
            if (dgViewData.SelectedCells.Count > 2)
            {
                MessageBox.Show("Необходимо выделить одну запись");
                return;
            }
            DataRowView rowView = dgViewData.SelectedValue as DataRowView;
            MessageBox.Show(rowView[0].ToString());
            string s;
            if (flag == 0)
            {
              s = "delete from Types where IDTypes=" + rowView[0].ToString();
            }
            else if (flag == 1)
            {
                s = "delete from Teachers where IDTeachers=" + rowView[0].ToString();
            }
            else
            {
                s = "delete from SubGroups where IDGroups=" + rowView[0].ToString();
            }
            ////////////////
            if (fwbd.ExecCommand(s))
            {
                MessageBox.Show("Удаление прошло успешно");
            }
            else
            {
                MessageBox.Show("не удалось");
                return;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //ADDTableWin f = new ADDTableWin(fwbd.connection, flag);
            //f.ShowDialog();
            BDrep f = new BDrep(fwbd.connection);
            f.FilingTeachers();
            MessageBox.Show("stop");
        }






    }
}
