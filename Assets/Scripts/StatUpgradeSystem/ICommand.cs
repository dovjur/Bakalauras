using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void ExecuteCommand();
    void UndoCommand();
}
