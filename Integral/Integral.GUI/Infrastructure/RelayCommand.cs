using System;
using System.Windows.Input;

namespace Integral.GUI.Infrastructure
{
    internal class RelayCommand : ICommand
    {
        private Action<object> action;
        private Func<object, bool> func;

        public RelayCommand(
            Action<object> action,
            Func<object, bool> func)
        {
            this.action = action;
            this.func = func;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return func(parameter);
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }
    }
}
