using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NetUseGui.ViewModel
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _onExecute;
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _onExecute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute.Invoke(parameter);
        public void Execute(object parameter) => _onExecute?.Invoke(parameter);
    }
}
