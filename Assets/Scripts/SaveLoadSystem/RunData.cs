using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RunData
{
    private int coinsCollected = 0;
    private int enemiesKilled = 0;
    private float timeSpent = 0;

    private static RunData instance;
    public static RunData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RunData();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

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

    public void EndOfRun(bool dead)
    {
        if (dead)
        {
            coinsCollected /= 2;
        }

        SaveData.Instance.player.coins += coinsCollected;
        SaveData.Instance.runData = this;
        SaveLoad.Save(SaveData.Instance);
    }

    public void ResetRun()
    {
        coinsCollected = 0;
        enemiesKilled = 0;
        timeSpent = 0;
    }
}
