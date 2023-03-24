using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability")]
public class AbilitySO : ScriptableObject
{
    public event Action OnUpgrade;

    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private float _baseDPS;
    [SerializeField, Range(1, 10)] private float _dpsMultiplier;

    [SerializeField, Multiline] private string _description;

    public Sprite DisplayIcon => _iconSprite;
    public string Title => _name;
    public int Level { get => _level; set => _level = value; }
    public string Description => _description;
    public float Cost => _baseCost; //unlock cost = level 1 price
    public float DPS { get; private set; }

    [SerializeField] private float _baseCost = 100f;
    [SerializeField, Range(1, 10)] private float _costMultiplier = 1.15f;

    public void Upgrade()
    {
        var upgrade = GetAvailableUpgrades();

        if (!upgrade.canAfford)
        {
            Debug.Log($"Need {upgrade.cost - GameManager.Instance.Gold} more gold");
            return;
        }

        GameManager.Instance.RemoveGold(upgrade.cost);
        _level += upgrade.upgrades;

        OnUpgrade?.Invoke();
    }

    public (int upgrades, float cost, bool canAfford) GetAvailableUpgrades()
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

        for (int i = nextUpgrade; i <= _level + upgradeTimes; i++)
        {
            totalCost += _baseCost * Mathf.Pow(i, _costMultiplier);
        }

        return totalCost;
    }
}