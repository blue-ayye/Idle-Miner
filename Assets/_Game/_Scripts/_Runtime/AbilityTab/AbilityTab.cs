using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityTab : TabBase
{
    [SerializeField] private List<AbilitySO> _abilities;
    [SerializeField] private AbilityUI _abilityUIPrefab;
    [SerializeField] private Transform _abilityUIContainer;

    public List<int> AbilityLevels
    {
        // Retrieve the current levels of each Ability object
        get => _abilities.Select(ability => ability.Level).ToList();

        // Set the levels based on a new list of values
        set => _abilities.Zip(value, (ability, level) => ability.Level = level).ToList();
    }

    public int UnlockedAbilityIndex { get; set; }

    private void Start()
    {
        GameManager.OnGoldChanged += GameManager_OnGoldChanged;

        for (int i = 0; i <= UnlockedAbilityIndex; i++)
        {
            CreateAbilityUI(_abilities[i]);
        }
    }

    private void GameManager_OnGoldChanged()
    {
        if (_abilities.Count <= UnlockedAbilityIndex + 1 ||
            _abilities[UnlockedAbilityIndex].Cost > GameManager.Instance.Gold)
        {
            return;
        }

        UnlockedAbilityIndex++;
        CreateAbilityUI(_abilities[UnlockedAbilityIndex]);
    }

    private void CreateAbilityUI(AbilitySO ability)
    {
        if (ability == null) return;

        var newAbilityUI = Instantiate(_abilityUIPrefab, _abilityUIContainer);
        newAbilityUI.Bind(ability);
        newAbilityUI.transform.SetAsLastSibling();
    }
}