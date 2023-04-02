using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> historyStack = new Stack<ICommand>();

    public void ExecuteCommand(ICommand action)
    {
        action.ExecuteCommand();
        historyStack.Push(action);
    }

    public void UndoCommand()
    {
        if (historyStack.Count > 0)
        {
            historyStack.Pop().UndoCommand();
        }
    }

    public int GetHistoryCount()
    {
        return historyStack.Count;
    }
}
