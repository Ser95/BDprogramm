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
    /// Логика взаимодействия для ClassRoomsWindow.xaml
    /// </summary>
    public partial class ClassRoomsWindow : Window
    {
        SqlConnection connection;
        DataTable dt;
        string  NormalStrokaTypes(string s)
        {
            string[] words = s.Split(new char[] { '+','!' });
            s = "";
            int i;
            for (i = 0; i < words.Count()-2; i++)
            {
               // MessageBox.Show(words[i]);
                s += words[i]+",";
            }
            s += words[i];
            return s;
        }
        void ZapGrid()
        {
            try
            {               
                SqlDataAdapter sqladapter;
                SqlCommandBuilder sqlcmd;                
                dgViewData.ItemsSource = null;
                sqladapter = new SqlDataAdapter("select *  from dbo.ClassRooms", connection);
                sqlcmd = new SqlCommandBuilder(sqladapter);
                dt = new DataTable();
                sqladapter.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][2] = NormalStrokaTypes(dt.Rows[i][2].ToString());
                }
                dgViewData.ItemsSource = dt.DefaultView;
                HiddenColumns();
                NormalStrokaTypes(dt.Rows[3][2].ToString());
            }
            catch
            {
                MessageBox.Show("ghjghj");
            }
        }
        public ClassRoomsWindow( SqlConnection connection)
        {
            InitializeComponent();
            this.connection=connection;
            ZapGrid();
        }
        private void HiddenColumns()
        {
            int i = 0;
            int Visiblecolumn = 3;
           
            foreach (DataGridColumn column in dgViewData.Columns)
            {
                if (i < Visiblecolumn) column.Visibility = Visibility.Visible;
                else column.Visibility = Visibility.Collapsed;
                i++;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // ZapGrid();
        }
    }
}
