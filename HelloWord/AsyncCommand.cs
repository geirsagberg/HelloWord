using System;
using Cirrious.MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace HelloWord
{

    /// <summary>
    /// Async command class that enables unit testing of commands that return a task. From http://stackoverflow.com/questions/15727515/how-to-unit-test-delegatecommand-that-calls-async-methods-in-mvvm.
    /// </summary>
    public class AsyncCommand : IMvxCommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<Task> ExecuteHandler { get; set; }

        protected Func<bool> CanExecuteHandler { get; set; }

        public AsyncCommand(Func<Task> executeHandler, Func<bool> canExecuteHandler = null)
        {
            if (executeHandler == null)
            {
                throw new ArgumentNullException("executeHandler");
            }

            ExecuteHandler = executeHandler;
            CanExecuteHandler = canExecuteHandler;
        }


        /// <summary>
        /// Async execute method for unit testing
        /// </summary>
        public Task ExecuteAsync()
        {
            return ExecuteHandler();
        }

        public bool CanExecute()
        {
            return CanExecuteHandler == null || CanExecuteHandler();
        }

        public void RaiseCanExecuteChanged()
        {
            var canExecuteHandler = CanExecuteChanged;
            if (canExecuteHandler != null)
            {
                canExecuteHandler(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public virtual async void Execute(object parameter)
        {
            if (CanExecute(parameter))
                await ExecuteAsync();
        }

        public async void Execute()
        {
            if (CanExecute())
                await ExecuteAsync();
        }
    }

    public class AsyncCommand<T> : IMvxCommand
    {
        public event EventHandler CanExecuteChanged;

        private Func<T, Task> ExecuteHandler { get; set; }

        protected Func<bool> CanExecuteHandler { get; set; }

        public AsyncCommand(Func<T, Task> executeHandler, Func<bool> canExecuteHandler = null)
        {
            if (executeHandler == null)
            {
                throw new ArgumentNullException("executeHandler");
            }

            ExecuteHandler = executeHandler;
            CanExecuteHandler = canExecuteHandler;
        }


        /// <summary>
        /// Async execute method for unit testing
        /// </summary>
        public Task ExecuteAsync(T parameter)
        {
            return ExecuteHandler(parameter);
        }

        public bool CanExecute()
        {
            return CanExecuteHandler == null || CanExecuteHandler();
        }

        public void RaiseCanExecuteChanged()
        {
            var canExecuteHandler = CanExecuteChanged;
            if (canExecuteHandler != null)
            {
                canExecuteHandler(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute();
        }

        public virtual async void Execute(object parameter)
        {
            if (!(parameter is T))
                throw new ArgumentException("Parameter must be of type " + typeof(T));
            if (CanExecute(parameter))
                await ExecuteAsync((T)parameter);
        }

        public async void Execute()
        {
            if (CanExecute())
                await ExecuteAsync(default(T));
        }
    }
}

