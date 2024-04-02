using System.Collections.ObjectModel;
using TL11.Database;
using TL11.Models;


namespace TL11;

public partial class Courses : ContentPage
{
	private DBClass db;
    private Course EditC = null;

    public Courses()
	{
		InitializeComponent();
        db = new DBClass();
        GetItems();
    }

    private void GetItems()
    {
        var cl = db.GetCourses();
        CourseLst.ItemsSource = "";
        CourseLst.ItemsSource = cl;
    }

    private void AddBtn_Clicked(object sender, EventArgs e)
    {
        if (CourseNameTxt.Text!="")
        {
            db.AddCourse(new Course { Name = CourseNameTxt.Text });
            GetItems();
        }
    }

    private void EditBtn_Clicked(object sender, EventArgs e)
    {
        if (CourseNameTxt.Text != "")
        {
            EditC.Name = CourseNameTxt.Text;
            db.UpdateCourse(EditC);
            GetItems();
            AddBtn.IsVisible = true;
            EditBtn.IsVisible = false;
            EditC = null;
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if  (btn != null && btn.CommandParameter is Course)
            {
            var c = (Course)btn.CommandParameter;
            db.DeleteCourse(c);
            GetItems();
            } 
    }
    private void Edit_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        if (btn != null && btn.CommandParameter is Course)
        {
            var c = (Course)btn.CommandParameter;
            CourseNameTxt.Text = c.Name;
            EditC = c;
            AddBtn.IsVisible = false;
            EditBtn.IsVisible = true;
        }
    }
}