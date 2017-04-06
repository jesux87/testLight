using System;
using System.Windows.Input;

namespace WpfTools
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> canExecuteMethod;
        private readonly Action<object> executeMethod;
        private bool isExecuting;

        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null)
        {
            if ((executeMethod == null) && (canExecuteMethod == null))
            {
                throw new ArgumentNullException("executeMethod", @"Execute Method cannot be null");
            }

            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
        
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteMethod == null)
            {
                return true;
            }

            return this.canExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            this.executeMethod(parameter);
        }
    }
}
