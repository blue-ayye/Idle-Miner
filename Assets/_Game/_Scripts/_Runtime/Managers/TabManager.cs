using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    public static TabManager Instance;

    #region Data Persistence

    [System.Serializable]
    public struct Data
    {
        public List<int> StatLevels;
        public List<int> PickaxeLevels;
        public int UnlockedPickaxeIndex;

        public Data(List<int> statLevels, List<int> pickaxeLevels, int unlockedPickaxeIndex)
        {
            StatLevels = statLevels;
            PickaxeLevels = pickaxeLevels;
            UnlockedPickaxeIndex = unlockedPickaxeIndex;
        }
    }

    public void SetData(Data data)
    {
        _pickaxeTab.PickaxeLevels = data.PickaxeLevels;
        _pickaxeTab.UnlockedPickaxeIndex = data.UnlockedPickaxeIndex;
    }

    public Data GetData()
    {
        return new Data(new List<int>(), _pickaxeTab.PickaxeLevels, _pickaxeTab.UnlockedPickaxeIndex);
    }

    #endregion Data Persistence

    [SerializeField] private StatsTab _statsTab;
    [SerializeField] private PickaxeTab _pickaxeTab;

    private List<TabBase> _tabs;

    private TabBase _activeTab;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _tabs = new List<TabBase> { _statsTab, _pickaxeTab };

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