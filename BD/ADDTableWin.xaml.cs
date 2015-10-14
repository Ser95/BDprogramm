using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для ADDTableWin.xaml
    /// </summary>
    public partial class ADDTableWin : Window
    {
        ForWorkBD fwbd;
        int flag;
        public ADDTableWin(SqlConnection connection,int flag)
        {
            InitializeComponent();
            fwbd = new ForWorkBD(connection);
            this.flag = flag;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           int n=fwbd.NumberID("select IDTypes from Types order by IDTypes Desc");
           if (n == -1)
           {
               MessageBox.Show("dvvsdf");
               return;
           }
          //добавить добавление элемента для всех
        }
    }
}
