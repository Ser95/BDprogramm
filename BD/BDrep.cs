using System;
using System.Collections;
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
       int[] MappingType;
       public BDrep(SqlConnection connection)
       {
           this.connection = connection;
           FilingTypes();
           FilingRooms();
       }
       public ClassRoom[] ClassRooms { get; private set; }
       public ClassRoomType[] ClassRoomsTypes { get; private set; }
        public StudentSubGroup[] StudentSubGroups { get; private set; }
        public Teacher[] Teachers { get; private set; }
     
       /* public void FilingTeachers()
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
            

        }*/

       public void FilingTypes()
        {
            DateTime start = DateTime.Now;
            SqlDataAdapter sqladapter = new SqlDataAdapter("select IDTypes,Description from Types order by IDTypes asc", connection);
            SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();
            sqladapter.Fill(dt);
            int n = dt.Rows.Count;
            ClassRoomsTypes = new ClassRoomType[n];
            MappingType = new int[n];

            for (int i = 0; i < n; i++)
            {
                ClassRoomsTypes[i] = new ClassRoomType(dt.Rows[i][1].ToString());
                MappingType[i] = Convert.ToInt32((dt.Rows[i][0].ToString()));              
            }
            DateTime now = DateTime.Now;
            TimeSpan time = now - start;
        }
       public void FilingRooms()
       {
           DateTime start = DateTime.Now;
           SqlDataAdapter sqladapter = new SqlDataAdapter("select IDrooms,Housing,Number from ClassRooms order by IDrooms asc", connection);
           SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
           DataTable dt = new DataTable();
           sqladapter.Fill(dt);
           int n = dt.Rows.Count;
           ClassRooms = new ClassRoom[n];
           for (int i = 0; i < n; i++)
           {
               //список типов всех получаем
               string s = "select IDTypes,flag from ClassRoomsTypes where IDrooms=" + dt.Rows[i][0] + " order by IDTypes asc";
               SqlDataAdapter sqladapter1 = new SqlDataAdapter(s, connection);
               SqlCommandBuilder sqlcmd1 = new SqlCommandBuilder(sqladapter1);
               DataTable dt1 = new DataTable();
               sqladapter1.Fill(dt1);
               ClassRoomType[] ClassRoomsTypes1 = new ClassRoomType[dt1.Rows.Count];
               BitArray m = new BitArray(dt1.Rows.Count, false);
               for (int j = 0; j < dt1.Rows.Count; j++)
               {
                   int tip = Convert.ToInt32(dt1.Rows[j][0].ToString());
                   int nomertype=0;
                   //получение номера типа в массивe
                   for (int h = 0; h < MappingType.Count(); h++)
                   {
                       if(MappingType[h]==tip)
                       {
                           nomertype = h;
                           h = MappingType.Count();
                           ClassRoomsTypes1[j] = ClassRoomsTypes[nomertype];
                           //проверка на обязательность
                           if (Convert.ToInt32(dt1.Rows[j][1].ToString()) == 1) m[i]=true ;
                       }
                   }                    
               }
               ClassRooms[i] = new ClassRoom(Convert.ToInt32(dt.Rows[i][2].ToString()), Convert.ToInt32(Convert.ToInt32(dt.Rows[i][1].ToString())), ClassRoomsTypes1, m);
           }
           DateTime now = DateTime.Now;
           TimeSpan time = now - start;


       }

    }
}
