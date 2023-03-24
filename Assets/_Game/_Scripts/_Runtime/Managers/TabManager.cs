using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    #region Data Persistence

    [System.Serializable]
    public struct Data
    {
        public List<int> StatLevels;
        public List<int> AbilityLevels;
        public int AbilityUnlockedIndex;

        public Data(List<int> statLevels, List<int> abilityLevels, int abilityUnlockedIndex)
        {
            StatLevels = statLevels;
            AbilityLevels = abilityLevels;
            AbilityUnlockedIndex = abilityUnlockedIndex;
        }
    }

    public void SetData(Data data)
    {
        _abilityTab.AbilityLevels = data.AbilityLevels;
        _abilityTab.UnlockedAbilityIndex = data.AbilityUnlockedIndex;
    }

    public Data GetData()
    {
        return new Data(new List<int>(), _abilityTab.AbilityLevels, _abilityTab.UnlockedAbilityIndex);
    }

    #endregion Data Persistence

    [SerializeField] private StatsTab _statsTab;
    [SerializeField] private AbilityTab _abilityTab;

    private List<TabBase> _tabs;

    private TabBase _activeTab;

    private void Start()
    {
        _tabs = new List<TabBase> { _statsTab, _abilityTab };

        //Disable all tabs
        foreach (var tab in _tabs)
        {
            tab.TabUI.SetActive(false);
        }

        SelectTab(1);
    }

    public void SelectTab(int index)
    {
        var tab = _tabs[index - 1];
        if (_activeTab == tab) return;

        _activeTab?.TabUI.SetActive(false);
        _activeTab = tab;
        _activeTab.TabUI.SetActive(true);
    }
}