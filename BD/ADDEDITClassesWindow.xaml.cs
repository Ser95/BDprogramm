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
    /// Логика взаимодействия для ADDEDITClassesWindow.xaml
    /// </summary>
    public partial class ADDEDITClassesWindow : Window
    {
        CheckBox[] cbTeachers, cbType,cbGroups;
        int[] mTeachers, mType, mGroups;
        ForWorkBD fwbd;
        int flag;
        int id;
        public ADDEDITClassesWindow(  SqlConnection connection,int flag,int id)
        {
            InitializeComponent();
            fwbd = new ForWorkBD(connection);
            this.flag = flag;
            this.id = id;
        }
        bool FillinfCheckBox(ref CheckBox[] ChB, ref int[] Mapping, string sqlcom, Grid grid,bool fl)
        {
            try
            {
                SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcom, fwbd.connection);
                SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
                DataTable dt = new DataTable();
                sqladapter.Fill(dt);
                ChB = new CheckBox[dt.Rows.Count];                
                Mapping = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)//выставить по высоте
                {
                    if (fl)
                    {
                        ChB[i] = new CheckBox() { Content = dt.Rows[i][1].ToString() };
                        ChB[i].Margin = new Thickness(10, 15 + 20 * i, 0, 0);
                        grid.Children.Add(ChB[i]);
                        Mapping[i] = Convert.ToInt32(dt.Rows[i][0].ToString());
                    }
                    else
                    {
                        ChB[i] = new CheckBox() { Content = dt.Rows[i][1].ToString() + dt.Rows[i][2].ToString() };
                        ChB[i].Margin = new Thickness(10, 15 + 20 * i, 0, 0);
                        grid.Children.Add(ChB[i]);
                        Mapping[i] = Convert.ToInt32(dt.Rows[i][0].ToString());
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }


        }
        void AddClick()
        {
            int k = fwbd.NumberID("select ID from Classes order by ID Desc");
            SqlCommand command = new SqlCommand();
            command.Connection = fwbd.connection;
            command.CommandText = "insert into Classes (ID,Name,Count) Values(@ID,@Name,@Count) ";
            command.Parameters.AddWithValue("@ID", k);
            command.Parameters.AddWithValue("@Name",tbName.Text);
            command.Parameters.AddWithValue("@Count", tbCount.Text);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ERROR");
                return;
            }
            for (int i = 0; i < cbTeachers.Count(); i++)
            {
                string s = "insert into ClassesTeachers(ID,IDTeachers) Values(@ID,@IDTeachers)";
                command = new SqlCommand(s);
                command.Connection = fwbd.connection;
                if (cbTeachers[i].IsChecked == true)
                {
                    command.Parameters.AddWithValue("@IDTeachers", mTeachers[i]);
                    command.Parameters.AddWithValue("@ID", k);
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
           for (int i = 0; i < cbType.Count(); i++)
            {
                string s = "insert into ClassesTypes(ID,IDTypes) Values(@ID,@IDTypes)";
                command = new SqlCommand(s);
                command.Connection = fwbd.connection;
                if (cbType[i].IsChecked == true)
                {
                    command.Parameters.AddWithValue("@IDTypes", mType[i]);
                    command.Parameters.AddWithValue("@ID", k);
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
            for (int i = 0; i < cbGroups.Count(); i++)
            {
                string s = "insert into ClassesSubGroups(ID,IDGroups) Values(@ID,@IDGroups)";
                command = new SqlCommand(s);
                command.Connection = fwbd.connection;
                if (cbGroups[i].IsChecked == true)
                {
                    command.Parameters.AddWithValue("@IDGroups", mGroups[i]);
                    command.Parameters.AddWithValue("@ID", k);
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
        void ADDload()
        {
            Grid grid1 = new Grid();
            if(!FillinfCheckBox(ref cbTeachers, ref mTeachers, "select IDTeachers,LName  from Teachers", grid1,true))
            {
                MessageBox.Show("error teachers");
                return;
            }
            scrTeacher.Content = grid1;
            grid1 = new Grid();
            if (!FillinfCheckBox(ref cbType, ref mType, "select IDTypes,Description  from Types", grid1,true))
            {
                MessageBox.Show("error type");
                return;
            }
            scrType.Content = grid1;
            grid1 = new Grid();
            if (!FillinfCheckBox(ref cbGroups, ref mGroups, "select IDGroups,NumberSubGroup,NameGroup  from SubGroups", grid1, false))
            {
                MessageBox.Show("error groups");
                return;
            }
            scrSubGroups.Content = grid1;
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            AddClick();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ADDload();
        }
    }
}
