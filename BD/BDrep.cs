﻿using System;
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
       int[] MappingType,MappingTeachers,MappingGroups;
       public BDrep(SqlConnection connection)
       {
           this.connection = connection;
           FilingTypes();
           FilingRooms();
           FilingGroups();
           FilingTeachers();
           FilingClasses();
       }
       public ClassRoom[] ClassRooms { get; private set; }
       public ClassRoomType[] ClassRoomsTypes { get; private set; }
        public StudentSubGroup[] StudentSubGroups { get; private set; }
        public Teacher[] Teachers { get; private set; }
        public StudentsClass[] St;
       public void FilingTeachers()
        {
            DateTime start = DateTime.Now;
            SqlDataAdapter sqladapter = new SqlDataAdapter("select IDTeachers,LName from Teachers", connection);
            SqlCommandBuilder  sqlcmd = new SqlCommandBuilder(sqladapter);
            DataTable dt = new DataTable();          
            sqladapter.Fill(dt);
            int n = dt.Rows.Count;
            Teachers = new Teacher[n];
            MappingTeachers = new int[n];
            for (int i = 0; i < n; i++)
            {
                Teachers[i] = new Teacher(Convert.ToInt32(dt.Rows[i][0].ToString()), dt.Rows[i][1].ToString());
                MappingTeachers[i] = Convert.ToInt32((dt.Rows[i][0].ToString()));
            }
            DateTime now = DateTime.Now;
            TimeSpan time = now - start; 
        }
       public void FilingGroups()
       {
           DateTime start = DateTime.Now;
           SqlDataAdapter sqladapter = new SqlDataAdapter("select IDGroups,NameGroup,NumberSubGroup from SubGroups", connection);
           SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
           DataTable dt = new DataTable();
           sqladapter.Fill(dt);
           int n = dt.Rows.Count;
           StudentSubGroups = new StudentSubGroup[n];
           MappingGroups = new int[n];
           for (int i = 0; i < n; i++)
           {
               StudentSubGroups[i] = new StudentSubGroup(dt.Rows[i][1].ToString(), Convert.ToByte(dt.Rows[i][2].ToString()));
               MappingGroups[i] = Convert.ToInt32((dt.Rows[i][0].ToString())); 
           }
           DateTime now = DateTime.Now;
           TimeSpan time = now - start;
       }
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
       void FilingClasses()
       {
           SqlDataAdapter sqladapter = new SqlDataAdapter("select ID,Name,Count from Classes order by ID asc", connection);
           SqlCommandBuilder sqlcmd = new SqlCommandBuilder(sqladapter);
           DataTable dt = new DataTable();
           sqladapter.Fill(dt);
           sqladapter = new SqlDataAdapter("select Sum(Count) from Classes ", connection);
           sqlcmd = new SqlCommandBuilder(sqladapter);
           DataTable dtcountn = new DataTable();
           sqladapter.Fill(dtcountn);
           int n =Convert.ToInt32(dtcountn.Rows[0][0].ToString());
           St = new StudentsClass[n];
           int kl = 0;
           for (int i = 0; i <dt.Rows.Count;i++ )
           {
               //список типов всех получаем
               string s = "select IDTypes from ClassesTypes where ID=" + dt.Rows[i][0] + " order by IDTypes asc";
               SqlDataAdapter sqladapter1 = new SqlDataAdapter(s, connection);
               SqlCommandBuilder sqlcmd1 = new SqlCommandBuilder(sqladapter1);
               DataTable dt1 = new DataTable();
               sqladapter1.Fill(dt1);
               ClassRoomType[] ClassRoomsTypes1 = new ClassRoomType[dt1.Rows.Count];
               for (int j = 0; j < dt1.Rows.Count; j++)
               {
                   int tip = Convert.ToInt32(dt1.Rows[j][0].ToString());
                   int nomertype = 0;
                   //получение номера типа в массивe
                   for (int h = 0; h < MappingType.Count(); h++)
                   {
                       if (MappingType[h] == tip)
                       {
                           nomertype = h;
                           h = MappingType.Count();
                           ClassRoomsTypes1[j] = ClassRoomsTypes[nomertype];                        
                       }
                   }
               }
               //////////////
               s = "select IDTeachers from ClassesTeachers where ID=" + dt.Rows[i][0] + " order by IDTeachers asc";
               sqladapter1 = new SqlDataAdapter(s, connection);
               sqlcmd1 = new SqlCommandBuilder(sqladapter1);
               dt1 = new DataTable();
               sqladapter1.Fill(dt1);
               Teacher[] T = new Teacher[dt1.Rows.Count];
               for (int j = 0; j < dt1.Rows.Count; j++)
               {
                   int tip = Convert.ToInt32(dt1.Rows[j][0].ToString());
                   int nomertype = 0;
                   //получение номера типа в массивe
                   for (int h = 0; h < MappingTeachers.Count(); h++)
                   {
                       if (MappingTeachers[h] == tip)
                       {
                           nomertype = h;
                           h = MappingTeachers.Count();
                          T[j] = Teachers[nomertype];
                       }
                   }
               }
               /////////////////
               s = "select IDGroups from ClassesSubGroups where ID=" + dt.Rows[i][0] + " order by IDGroups asc";
               sqladapter1 = new SqlDataAdapter(s, connection);
               sqlcmd1 = new SqlCommandBuilder(sqladapter1);
               dt1 = new DataTable();
               sqladapter1.Fill(dt1);
               StudentSubGroup[] S = new StudentSubGroup[dt1.Rows.Count];
               for (int j = 0; j < dt1.Rows.Count; j++)
               {
                   int tip = Convert.ToInt32(dt1.Rows[j][0].ToString());
                   int nomertype = 0;
                   
                   //получение номера типа в массивe
                   for (int h = 0; h < MappingGroups.Count(); h++)
                   {
                       if (MappingGroups[h] == tip)
                       {
                           nomertype = h;
                           h = MappingGroups.Count();
                           S[j] = StudentSubGroups[nomertype];
                       }
                   }
               }
               for (int t=0; t < Convert.ToInt32(dt.Rows[0][2].ToString());t++)
			{
                St[kl] = new StudentsClass(S, T, dt.Rows[0][1].ToString(), ClassRoomsTypes1);
                kl++;
			}
             




           }
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
                           if (Convert.ToInt32(dt1.Rows[j][1].ToString()) == 1) m[j]=true ;
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
