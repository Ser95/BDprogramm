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

namespace BD
{
    /// <summary>
    /// Логика взаимодействия для ADDCLRWIN.xaml
    /// </summary>
    public partial class ADDCLRWIN : Window
    {
        public ADDCLRWIN()
        {
            InitializeComponent();
            for (int i = 1; i < 10; i++)
            {
                //CheckBox cb = new CheckBox() { Content = "CheckBox" + i.ToString() };
                //cb.Margin = new Thickness(10, 15 + 45 * i, 0, 0);
                //qwer.Children.Add(cb);
            }
        }
    }
}
