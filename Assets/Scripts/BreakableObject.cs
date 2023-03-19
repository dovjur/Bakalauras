using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Coin loot;

    private int coinCount;

    void Start()
    {
        coinCount = Random.Range(0,5);
    }

    public void Break()
    {
        animator.SetTrigger("Broken");
        loot.SpawnCoins(coinCount,transform);
        StartCoroutine(BreakCoroutine());
    }

    private IEnumerator BreakCoroutine()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
