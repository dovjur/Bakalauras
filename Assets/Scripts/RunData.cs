using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunData
{
    private static RunData _current;
    public static RunData current
    {
        get
        {
            if (_current == null)
            {
                _current = new RunData();
            }
            return _current;
        }
        set
        {
            _current = value;
        }
    }

    public int coinsCollected = 0;
    public int enemiesKilled = 0;

    public void AddCoins()
    {
        coinsCollected++;
    }
}
