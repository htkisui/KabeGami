﻿using System.Windows.Input;

namespace KabeGami.Desktop.ViewModels.Common.Primitives;

internal sealed class RelayCommand 
    : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Predicate<object?>? _canExecute;

    public RelayCommand(Action<object?> execute) : this(execute, null) { }

    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) 
        => _canExecute == null || _canExecute(parameter);

    public void Execute(object? parameter) 
        => _execute(parameter);
}
