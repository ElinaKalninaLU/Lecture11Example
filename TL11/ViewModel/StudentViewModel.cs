using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using TL11.Models;
using TL11.Database;

namespace TL11
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        public DBClass db;
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public event PropertyChangedEventHandler? PropertyChanged;
   
        private Student s;

        private string name;
        private string surname;

        private StudentsViewModel parent;

        public void Init()
        {
            SaveCommand = new RelayCommand(AddOrEdit); 
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            AddCommand = new RelayCommand(Add);
            if (db == null)
                db = ServiceProviderLocator.ServiceProvider.GetRequiredService<DBClass>();
        }
        public StudentViewModel() 
        {
            Init();
        }

        public StudentViewModel(Student student, DBClass _db, StudentsViewModel _parent) {
            s= student;
            db = _db;
            Name = student?.Name ?? "";
            Surname = student?.Surname ?? "";
            parent = _parent;
            Init();
        }


        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Surname
        {
            get { return surname; }
            set { SetProperty(ref surname, value); }
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddOrEdit()
        {

        }

        public void Edit()
        {
            if (s != null)
            {
                parent.SV = this;
            }
        }

        public void Delete()
        {
            if (s != null)
            {
                db.DeleteStudent(s);
                parent.DeleteComplete(this);
            }
        }

        public void Add()
        {
            if (s != null)
            {
                s.Name = Name;
                s.Surname = Surname;
                db.UpdateStudent(s);
            }
            else
            {
                s = new Student() { Name = this.Name, Surname = this.Surname };
                db.AddStudent(s);
            }
            parent.AddEditComplete(this);
        }
    }
}
