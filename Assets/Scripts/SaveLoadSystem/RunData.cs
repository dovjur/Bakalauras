using System;
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

    private int coinsCollected = 0;
    private int enemiesKilled = 0;
    private float timeSpent = 0;

    public void AddCoin()
    {
        coinsCollected++;
    }

    public int GetCoins()
    {
        return coinsCollected;
    }

    public void AddKill()
    {
        enemiesKilled++;
    }

    public int GetKills()
    {
        return enemiesKilled;
    }

    public void SetTime(float time)
    {
        timeSpent = time;
    }

    public float GetTime()
    {
        return timeSpent;
    }

    public void EndOfRun()
    {
        SaveData.Instance.player.coins += coinsCollected;
        SaveLoadSystem.Save(SaveData.Instance);
    }

    public void ResetInstance()
    {
        coinsCollected = 0;
        enemiesKilled = 0;
        timeSpent = 0;
    }
}
