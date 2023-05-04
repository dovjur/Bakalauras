using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagnetBuff", menuName = "Buff/MagnetBuff", order = 1)]
public class MagnetBuff : BuffData
{
    public override void ApplyBuff()
    {
        GameManager.Player.isMagnetOn = true;
    }
}
