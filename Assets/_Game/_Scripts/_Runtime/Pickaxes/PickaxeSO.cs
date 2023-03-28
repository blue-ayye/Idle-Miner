using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pickaxe")]
public class PickaxeSO : ScriptableObject
{
    public event Action OnUpgrade;

    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private float _baseDPS;
    [SerializeField, Range(1, 10)] private float _dpsMultiplier;
    [SerializeField] private float _baseCost = 100f;
    [SerializeField, Range(1, 10)] private float _costMultiplier = 1.15f;
    [SerializeField, Multiline] private string _description;

    public int Level { get => _level; set => _level = value; }
    public float DPS { get; private set; }
    public Sprite DisplayIcon => _iconSprite;
    public string DisplayName => _name;
    public string Description => _description;
    public float UnlockCost => _baseCost;

    public void Upgrade()
    {
        var upgrades = GetAvailableUpgrades();

        if (!upgrades.canAfford)
        {
            Debug.Log($"Need {upgrades.cost - GameManager.Instance.Gold} more gold");
            return;
        }

        GameManager.Instance.RemoveGold(upgrades.cost);
        _level += upgrades.upgradeTimes;

        OnUpgrade?.Invoke();
    }

    public (int upgradeTimes, float cost, bool canAfford) GetAvailableUpgrades()
    {
        float remainingGold = GameManager.Instance.Gold;
        int upgradeTimes = (int)GameManager.Instance.UpgradeTimes;
        float cost = CalculateCost(upgradeTimes);

        while (cost > remainingGold && upgradeTimes > 1)
        {
            upgradeTimes--;
            cost = CalculateCost(upgradeTimes);
        }

        bool canAfford = cost <= remainingGold;
        return (upgradeTimes, cost, canAfford);
    }

    private float CalculateCost(int upgradeTimes)
    {
        float totalCost = 0;
        int nextUpgrade = _level + 1;
        int lastUpgrade = _level + upgradeTimes;

        for (int i = nextUpgrade; i <= lastUpgrade; i++)
        {
            totalCost += _baseCost * Mathf.Pow(i, _costMultiplier);
        }

        return totalCost;
    }
}