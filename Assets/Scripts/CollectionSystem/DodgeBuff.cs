using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DodgeBuff", menuName = "Buff/DodgeBuff", order = 1)]
public class DodgeBuff : BuffData
{
    public float dodgeChance;

    public override void ApplyBuff()
    {
        GameManager.Player.dodgeChance += dodgeChance;
    }
}
