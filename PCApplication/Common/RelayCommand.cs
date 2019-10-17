using System;
using System.Diagnostics;
using System.Windows.Input;

namespace PCApplication.Commands {
    /// <summary>
    /// A relay command class implementing the ICommand interface, enabling command binding within views.
    /// It provides a reusable way to describe commands.
    /// </summary>
    public class RelayCommand : ICommand {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, Func<bool> canexecute = null) {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            _execute = execute;
            _canExecute = canexecute ?? (() => true);
        }

        [DebuggerStepThrough]
        public bool CanExecute(object p = null) {
            try { return _canExecute(); } catch { return false; }
        }

        public void Execute(object p = null) {
            if (!CanExecute(p))
                return;
            try { _execute(); } catch { Debugger.Break(); }
        }

        // Manual firing of the CanExecuteChanged ICommand event handler
        // instead of the automatic firing through the CommandManager
        public void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
