namespace ClassLibrary.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    internal class Command
    {
        private readonly Predicate<object> _canExecute;
        public Action<object> _execute;
        public event EventHandler CanExecuteChanged;
        public Command(Action<object> execute)
            : this(execute, null) { }
        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged.Invoke(this, EventArgs.Empty);
        }
    }
}
