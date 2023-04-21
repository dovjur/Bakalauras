using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{ 
    [SerializeField]
    private Sprite tabIdle;
    [SerializeField]
    private Sprite tabSelected;
    [SerializeField]
    private Sprite tabHover;
    [SerializeField]
    private List<GameObject> pages;

    private List<TabButton> tabButtons;
    private TabButton selectedTab;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
            button.background.sprite = tabSelected;
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        } 
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < pages.Count; i++)
        {
            if (i == index)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    private void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
            {
                continue;
            }
            else
            {
                button.background.sprite = tabIdle;
            }
        }
    }
}
