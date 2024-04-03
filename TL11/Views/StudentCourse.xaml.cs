using System.Collections.ObjectModel;
using TL11.Database;
using TL11.Models;

namespace TL11;

public partial class StudentCourse : ContentPage
{
    public ObservableCollection<Student> SList;
    public ObservableCollection<Course> CList;
    private DBClass db;
    public StudentCourse()
	{
		InitializeComponent();
         db = ServiceProviderLocator.ServiceProvider.GetRequiredService<DBClass>();
        if (db == null) db = new DBClass();

        LoadData();

    }

    public void LoadData()
    {
        CList = new ObservableCollection<Course>();
        List<Course> list = db.GetCourses();
        foreach (Course c in list)
            CList.Add(c);
        CourseCbo.ItemsSource = CList;
    }

    private void CourseCbo_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItems();
    }

    public void GetItems()
    {

        Course? c = CourseCbo.SelectedItem as Course;
        SList = new ObservableCollection<Student>();
        if (c != null)
        {
            var addStudentList = db.GetStudents();
            var stList = db.GetStudentsForCourse(c);
            foreach (var s in stList)
            {
                SList.Add(s);
                addStudentList.RemoveAll(a => a.ID == s.ID);
            }
            StudentLst.ItemsSource = SList;
            StudentCbo.ItemsSource = addStudentList;
        }
    }
    private void AddStudentBtn_Clicked(object sender, EventArgs e)
    {
        Course c = CourseCbo.SelectedItem as Course;
        Student s = StudentCbo.SelectedItem as Student;
        if (c != null && s!=null) 
        { 
            db.AddStudentCourse(s, c);
        }
        GetItems();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        Course c = CourseCbo.SelectedItem as Course;
        if (btn != null && btn.CommandParameter is Student && c != null)
        {
            var s = (Student)btn.CommandParameter;
            db.DeleteStudentCourse(s, c);
            GetItems();
        }
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        LoadData();
    }
}