using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private List<LootCard> lootCards = new List<LootCard>();

    private Animator animator;
    private bool isInRange;

    public LootCard LootCard;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        animator.SetBool("Open", true);
        LootCard = GetDroppedLoot();
    }

    private LootCard GetDroppedLoot()
    {
        int randomNumber = Random.Range(1, 101);
        Debug.Log(randomNumber);
        List<LootCard> possibleLoot = new List<LootCard>();
        foreach (LootCard lootCard in lootCards)
        {
            if (randomNumber <= lootCard.dropChance)
            {
                possibleLoot.Add(lootCard);
            }
        }
        if (possibleLoot.Count > 0)
        {
            LootCard droppedCard = possibleLoot[Random.Range(0, possibleLoot.Count)];
            Debug.Log(droppedCard);
            return droppedCard;
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
