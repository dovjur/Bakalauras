using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionDisplay : MonoBehaviour
{
    public List<Collection> collections;

    private int currentPage = 0;
    private List<GameObject> pagePanels = new List<GameObject>();

    public GameObject pagePanelPrefab;
    public Transform leftPanelParent;
    public Transform rightPanelParent;

    public GameObject prevButton;
    public GameObject nextButton;

    public void Start()
    {
        for (int i = 0; i < collections.Count; i += 2)
        {
            GameObject leftPagePanel = Instantiate(pagePanelPrefab, leftPanelParent);
            PopulatePage(leftPagePanel, i);
            pagePanels.Add(leftPagePanel);
            leftPagePanel.SetActive(false);

            if (i+1 < collections.Count)
            {
                GameObject rightPagePanel = Instantiate(pagePanelPrefab, rightPanelParent);
                PopulatePage(rightPagePanel, i + 1);

                pagePanels.Add(rightPagePanel);
                rightPagePanel.SetActive(false);
            }
        }

        UpdatePage(currentPage);
    }

    public void NextPage()
    {
        currentPage = Mathf.Min(currentPage + 2, pagePanels.Count - 1);
        UpdatePage(currentPage);
    }

    public void PrevPage()
    {
        currentPage = Mathf.Max(currentPage - 2, 0);
        UpdatePage(currentPage);
    }

    private void UpdatePage(int pageIndex)
    {
        foreach (GameObject panel in pagePanels)
        {
            panel.SetActive(false);
        }

        pagePanels[pageIndex].SetActive(true);
        if (pageIndex + 1 < pagePanels.Count)
        {
            pagePanels[pageIndex + 1].SetActive(true);
        }

        prevButton.SetActive(pageIndex > 0);
        nextButton.SetActive(pageIndex < pagePanels.Count - 2);
    }

    private void PopulatePage(GameObject pagePanel, int startIndex)
    {
        int endIndex = Mathf.Min(startIndex + 1, collections.Count - 1);

        for (int i = startIndex; i <= endIndex; i++)
        {
            Collection collection = collections[i];

            CollectionPanel collectionPanel = pagePanel.GetComponentInChildren<CollectionPanel>();
            PopulatePanel(collectionPanel, collection);
        }
    }

    private void PopulatePanel(CollectionPanel panel, Collection collection)
    {
        panel.SetName(collection.collectionName);

        foreach (LootCard lootCard in collection.collectionCards)
        {
            //GameObject cardObject = Instantiate(cardPrefab, panel.contentPanel.transform);
            //cardObject.GetComponent<Card>().SetName(lootCard.cardName);
            //cardObject.GetComponent<Card>().SetImage(lootCard.cardImage);
        }
    }
}
