using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private float _goldBalance;
    [SerializeField] private List<TabBaseUI> _tabs;

    private TabBaseUI _activeTab;

    private void Start()
    {
        //Disable all tabs
        foreach (var tab in _tabs)
        {
            tab.gameObject.SetActive(false);
        }

        SelectTab(1);
    }

    public void SelectTab(int index)
    {
        var tab = _tabs[index - 1];
        if (_activeTab == tab) return;

        _activeTab?.gameObject.SetActive(false);
        _activeTab = tab;
        _activeTab.gameObject.SetActive(true);
    }
}
