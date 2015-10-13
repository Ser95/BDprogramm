using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BD
{
    public class ForWorkBD
    {
        SqlConnection connection;
        SqlDataAdapter sqladapter;
        SqlCommandBuilder sqlcmd;
        DataTable dt;
        public ForWorkBD(SqlConnection connection)
        {
            this.connection=connection;
        }
        public bool FillingDataGrid(DataGrid DG, string sqlcom, string name)
        {
            try
            {
                DG.ItemsSource = null;
                sqladapter = new SqlDataAdapter(sqlcom, connection);
                sqlcmd = new SqlCommandBuilder(sqladapter);
                dt = new DataTable();
                dt.TableName = name;
                sqladapter.Fill(dt);
                DG.ItemsSource = dt.DefaultView;
                return true;
            }
            catch
            {
               return false;
            }
          
        }
        public bool SaveDataGrid(DataGrid DG, string name)
        {
            if (dt.TableName == name)
            {
                try
                {
                    sqladapter.Update(dt);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else return false;
           
        }
        public bool FillinfCheckBox(ref CheckBox[] ChB,string sqlcom,Grid grid)
        {
            try
            {
                SqlDataAdapter sqladapter = new SqlDataAdapter(sqlcom, connection);
                SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
                DataTable dt = new DataTable();
                sqladapter.Fill(dt);
                ChB = new CheckBox[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)//выставить по высоте
                {
                    ChB[i] = new CheckBox() { Content = dt.Rows[i][0].ToString() };
                    ChB[i].Margin = new Thickness(10, 15 + 45 * i, 0, 0);
                    grid.Children.Add(ChB[i]);
                }
                return true;
            }
            catch
            {
                return false;
            }


        }



    }
}
