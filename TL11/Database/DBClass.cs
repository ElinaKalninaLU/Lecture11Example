using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TL11.Models;

namespace TL11.Database
{
    public class DBClass
    {
        SQLiteConnection conn;

        public static string dbFileN = "courses.db";
        public static string folderPath 
            = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static string dbPath {
            get
            {
                if (DeviceInfo.Platform.ToString() == "Android")
                {
                    Debug.WriteLine(DeviceInfo.Platform);
                    return Path.Combine(FileSystem.AppDataDirectory, dbFileN);
                }
                else
                {
                    return Path.Combine(folderPath, dbFileN);
                }
            }
            }
     //  Path.Combine(FileSystem.AppDataDirectory, dbFileN);

        public bool Init()
        { 
            if (conn != null) { return false; }
            try
            {
                conn = new SQLiteConnection(dbPath);
                var r = conn.CreateTable<Course>();
                return true;
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine("Error" + ex);
                return false; 
            }
        }

        public bool CreateTestData()
        {
            try
            {
                Init();
                int count = conn?.Table<Course>()?.Count() ?? 0;
                if (count == 0)
                {
                    AddCourse(new Course { Name = ".NET" });
                    AddCourse(new Course { Name = "AI" });
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return false;
            }
        }

        public List<Course> GetCourses()
        {
            try {
                Init();
                List<Course> list = conn.Table<Course>().ToList();
                return list;
            }
            catch (Exception ex)
            { 
                Debug.WriteLine("Error" + ex);
                return null;
            }
        
        }

        public bool AddCourse(Course c)
        {
            try 
            {
                Init();
                int i = conn.Insert(c);
                if (i == 1) { return true; }
                else return false;
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine("Error" + ex); 
                return  false; 
            }
        }

        public bool DeleteCourse(Course c)
        {
            try
            {
                Init();
                int i = conn.Delete(c);
                if (i == 1) { return true; }
                else return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return false;
            }
        }

        public bool UpdateCourse(Course c)
        {
            try
            {
                Init();
                int i = conn.Update(c);
                if (i == 1) { return true; }
                else return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return false;
            }
        }
    }
}
