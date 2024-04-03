using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TL11
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        string name;
        string surname;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name
        {
            set { SetProperty(ref name, value); }
            get { return name; }
        }
        public string Surname
        {
            set { SetProperty(ref surname, value); }
            get { return surname; }
        }
        public override string ToString() { return Name + ", " + Surname; }
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
    }
}

