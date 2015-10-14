using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD
{
   public  class BDrep
    {
       SqlConnection connection;
       public BDrep(SqlConnection connection)
       {
           this.connection = connection;
       }
       public ClassRoomType[] ClassRoomsTypes { get; private set; }
        public StudentSubGroup[] StudentSubGroups { get; private set; }
        public Teacher[] Teachers { get; private set; }
      //  public ClassRoom[] ClassRooms { get; private set; }
        public void FilingTeachers()
        {
            DateTime start = DateTime.Now;
            SqlDataAdapter sqladapter = new SqlDataAdapter("select * from Teachers", connection);
            SqlCommandBuilder  sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();          
            sqladapter.Fill(dt);
            int n = dt.Rows.Count;
            Teachers = new Teacher[n];
            for (int i = 0; i < n; i++)
            {
                Teachers[i] = new Teacher(Convert.ToInt32(dt.Rows[i][0].ToString()), dt.Rows[i][1].ToString());
            }
            DateTime now = DateTime.Now;
            TimeSpan time = now - start;
            

        }



    }
}
