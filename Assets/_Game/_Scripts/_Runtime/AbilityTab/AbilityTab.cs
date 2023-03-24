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
        foreach (var ability in _abilities)
        {
            CreateAbilityUI(ability);
        }
    }

    private void CreateAbilityUI(AbilitySO ability)
    {
        var newAbilityUI = Instantiate(_abilityUIPrefab, _abilityUIContainer);
        newAbilityUI.Bind(ability);
    }
}