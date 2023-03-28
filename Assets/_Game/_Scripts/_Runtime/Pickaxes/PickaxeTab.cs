using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickaxeTab : TabBase
{
    [SerializeField] private List<PickaxeSO> _pickaxes;
    [SerializeField] private PickaxeUI _pickaxeUIPrefab;
    [SerializeField] private Transform _pickaxeUIContainer;

    public List<int> PickaxeLevels
    {
        // Retrieve the current levels of each Ability object
        get => _pickaxes.Select(p => p.Level).ToList();

        // Set the levels based on a new list of values
        set => _pickaxes.Zip(value, (p, lv) => p.Level = lv).ToList();
    }

    public int UnlockedPickaxeIndex { get; set; }

    private void Start()
    {
        GameManager.OnGoldChanged += GameManager_OnGoldChanged;

        for (int i = 0; i <= UnlockedPickaxeIndex; i++)
        {
            CreatePickaxeUI(_pickaxes[i]);
        }
    }

    private void GameManager_OnGoldChanged()
    {
        if (_pickaxes.Count <= UnlockedPickaxeIndex + 1 ||
            _pickaxes[UnlockedPickaxeIndex].UnlockCost > GameManager.Instance.Gold)
        {
            return;
        }

        UnlockedPickaxeIndex++;
        CreatePickaxeUI(_pickaxes[UnlockedPickaxeIndex]);
    }

    private void CreatePickaxeUI(PickaxeSO pickaxe)
    {
        if (pickaxe == null) return;

        var newPickaxeUI = Instantiate(_pickaxeUIPrefab, _pickaxeUIContainer);
        newPickaxeUI.Bind(pickaxe);
        newPickaxeUI.transform.SetAsFirstSibling();
    }
}