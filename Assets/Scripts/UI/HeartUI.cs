using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab;
    [SerializeField]
    private GameObject heartPanel;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite emptyHeart;

    private Character character;
    private List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        character = GameManager.Player;
        LoadHearts();
    }
    private void OnEnable()
    {
        Player.onHealthChange += UpdateHearts;
    }
    private void OnDisable()
    {
        Player.onHealthChange -= UpdateHearts;
    }

    private void LoadHearts()
    {
        for (int i = 0; i < character.health; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartPanel.transform);
            hearts.Add(heart);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }
}
