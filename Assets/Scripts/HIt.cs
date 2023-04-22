using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIt : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(player.strenght);
        }
        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<BreakableObject>().Break();
        }
    }
}
