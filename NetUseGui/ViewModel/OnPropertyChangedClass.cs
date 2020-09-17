using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace NetUseGui.ViewModel
{
    public class OnPropertyChangedClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            string[] names = prop.Split("\\/\r \n()\"\'-".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (names.Length != 0)
                foreach (var val in names)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(val));
        }

        public void OnPropertyChanged(IEnumerable<string> propList)
        {
            foreach (var val in propList.Where(name => !string.IsNullOrWhiteSpace(name)))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(val));
        }

        public void OnPropertyChanged(IEnumerable<PropertyInfo> propList)
        {
            foreach (var val in propList)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(val.Name));
        }

        public void OnAllPropertyChanged() => OnPropertyChanged(GetType().GetProperties());
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
    }
}
