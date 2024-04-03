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
                r = conn.CreateTable<Student>();
                 r = conn.CreateTable<StudentCourse>();
                CreateTestData();
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
                count = 0;
                count = conn?.Table<Student>()?.Count() ?? 0;
                if (count == 0)
                {
                    AddStudent(new Student { Name = "Joe", Surname="Baiden" });
                    AddStudent(new Student { Name = "Donald", Surname="Tramp" });
                }
                count = 0;
                count = conn?.Table<StudentCourse>()?.Count() ?? 0;
                if (count == 0)
                {
                    foreach (Course c in GetCourses())
                    {
                        foreach(Student s in GetStudents())
                        {
                            AddStudentCourse(s, c);
                        }
                    }
                    
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

        public List<Student> GetStudents()
        {
            try
            {
                Init();
                List<Student> list = conn.Table<Student>().ToList();
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

        public bool AddStudent(Student s)
        {
            try
            {
                Init();
                int i = conn.Insert(s);
                if (i == 1) { return true; }
                else return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return false;
            }
        }

        public bool AddStudentCourse(Student s, Course c)
        {
            try
            {
                var sc = new Models.StudentCourse() { Course_ID = c.ID, Student_ID = s.ID };
                return AddStudentCourse(sc);
             }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return false;
            }
        }

        public bool AddStudentCourse(Models.StudentCourse sc)
        {
            try
            {
                Init();
                int i = conn.Insert(sc);
                if (i == 1) { return true; }
                else return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return false;
            }
        }

        public bool DeleteCourse(Course c)
        {
            try
            {
                Init();
                DeleteStudentCourse(null, c);
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

        public bool DeleteStudent(Student s)
        {
            try
            {
                Init();
                DeleteStudentCourse(s);
                int i = conn.Delete(s);
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

        public bool UpdateStudent(Student s)
        {
            try
            {
                Init();
                int i = conn.Update(s);
                if (i == 1) { return true; }
                else return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return false;
            }
        }

        public List<Student> GetStudentsForCourse(Course c)
        {
            try
            {
                Init();
                List<int> studentIds = conn.Table<Models.StudentCourse>()
                    .Where( sc => sc.Course_ID == c.ID)
                    .Select(sc => sc.Student_ID).ToList();
                return conn.Table<Student>()
                        .Where (s => studentIds.Contains(s.ID)).ToList();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return null;
            }
        }

        public bool DeleteStudentCourse(Student s = null, Course c = null)
        {
            try {
                List<Models.StudentCourse> scList;
                if (s != null && c != null)
                {
                    scList = conn.Table<Models.StudentCourse>()
                                   .Where(sc => sc.Student_ID == s.ID
                                        && sc.Course_ID == c.ID).ToList();
                }
                else if (s != null && c is null)
                {
                    scList = conn.Table<Models.StudentCourse>()
                                .Where(sc => sc.Student_ID == s.ID).ToList();
                }
                else if (s is null && c != null)
                {
                    scList = conn.Table<Models.StudentCourse>()
                                .Where(sc => sc.Course_ID == c.ID).ToList();
                }
                else return false;
                foreach (Models.StudentCourse current in scList)
                {
                    conn.Delete(current);
                }
                return true;
                }
            catch (Exception ex)
            {
                Debug.WriteLine("Error" + ex);
                return false;
            }
        }
    }
}
