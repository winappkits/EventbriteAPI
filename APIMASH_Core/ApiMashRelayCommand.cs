
//-----------------------------------------------------------------------------
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace APIMASH_Core
{
    public class RelayCommand<T> : ICommand
    {
        private Predicate<T> _canExecute;
        private Action<T> _execute;        
        private bool _isExecuting = false;

        
        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }
        
        public void Execute(T parameter)
        {
            _execute(parameter);
        }

        bool ICommand.CanExecute(object parameter)
        {        
            return !_isExecuting && _canExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
       
        void ICommand.Execute(object parameter)
        {
            if (_canExecute((T)parameter))
            {
                _isExecuting = true;
                _execute((T)parameter);
                _isExecuting = false;
            }
            
        }
    }

}
