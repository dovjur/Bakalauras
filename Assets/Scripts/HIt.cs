using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1);
        }
        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<BreakableObject>().Break();
        }
    }
}
