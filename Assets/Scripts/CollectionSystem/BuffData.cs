using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData : ScriptableObject
{
    public string buffDescription;

    public virtual void ApplyBuff() { }
}
