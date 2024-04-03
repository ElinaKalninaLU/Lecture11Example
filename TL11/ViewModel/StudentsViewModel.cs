using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TL11.Database;
using TL11.Models;

namespace TL11
{
    public class StudentsViewModel : INotifyPropertyChanged
    {
        protected DBClass db;
        public StudentViewModel SV
        {
            get { return sv; }
            set { SetProperty(ref sv, value); }
        }
        
        private StudentViewModel sv;

        private ObservableCollection<StudentViewModel> sList;

        public ObservableCollection<StudentViewModel> SList
        {
            get { return sList; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public StudentsViewModel()
        {
            Init();
        }
        public void Init()
        {
            sList = new ObservableCollection<StudentViewModel>();
            db = ServiceProviderLocator.ServiceProvider.GetRequiredService<DBClass>();
            if (db == null) db = new DBClass();
            foreach (Student si in db.GetStudents().ToList())
            {  
                sList.Add(new StudentViewModel(si, db, this)); 
            }
            SV = new StudentViewModel(null, db, this);
        }

        public DBClass DBC
        {
            get { return db; }
            set
            {
                db = value;
                // NotifyPropertyChanged for DbContext if necessary
            }
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

        public void AddEditComplete(StudentViewModel sv)
        {
            if (!SList.Contains(sv))   
                    SList.Add(sv);
            SV = new StudentViewModel(null, db, this);
        }

        public void DeleteComplete(StudentViewModel sv)
        {
            SList.Remove(sv);
        }
    }
}
