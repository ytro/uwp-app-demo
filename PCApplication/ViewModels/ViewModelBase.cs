using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PCApplication.ViewModels
{
    /// <summary>
    /// The base ViewModel class.
    /// The base vm, which all vms inherit, implements the necessary INotifyPropertyChanged interface so it doesn't have to be done by each vm.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Notifies all property listeners (in the views) that a specified property has been changed.
        // [CallerMemberName] is a shortcut to express the property's name w/o needing to explicitly write the name in the code.
        protected void RaisePropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
