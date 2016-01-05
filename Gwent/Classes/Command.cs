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
        }

        public virtual void OnCanExecuteChanged()
        {
            this.CanExecuteChanged.Invoke(this, EventArgs.Empty);
        }
    }
}
