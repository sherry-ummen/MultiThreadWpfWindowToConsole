using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace DifferentThreadAkka.ViewModel {
    public abstract class ViewModelBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        // TODO use [CallerMemberName]
        protected void OnPropertyChanged(string propertyName) {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs args) {
            var handler = PropertyChanged;
            if (handler != null) handler(this, args);
        }
    }
}
