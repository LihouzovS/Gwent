namespace ClassLibrary.Classes
{
    using System;
    using System.Windows.Input;
        internal class Command : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
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
        public Command(Action<object> execute)
            : this(execute, null) { }
        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
            {
                return true;
            }

            return this._canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
            // был вариант с _execute.Invoke(parameter); но это одно и то же, вроде как
        }
    }
}
