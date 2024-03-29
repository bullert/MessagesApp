﻿using MessagesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MessagesApp.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<bool> _canExecute;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        //public event EventHandler CanExecuteChanged
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        //private Action _execute;
        //private Func<bool> _canExecute;

        //public RelayCommand(Action execute) : this(execute, null) { }

        //public RelayCommand(Action execute, Func<bool> canExecute)
        //{
        //    _execute = execute;
        //    _canExecute = canExecute;
        //}

        ////public event EventHandler CanExecuteChanged
        ////{
        ////    add { CommandManager.RequerySuggested += value; }
        ////    remove { CommandManager.RequerySuggested -= value; }
        ////}

        //public event EventHandler CanExecuteChanged;

        //public bool CanExecute(object parameter)
        //{
        //    return _canExecute();
        //}

        //public void Execute(object parameter)
        //{
        //    _execute();
        //}
    }
}
