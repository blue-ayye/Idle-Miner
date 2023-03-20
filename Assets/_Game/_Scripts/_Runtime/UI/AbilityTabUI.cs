using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTabUI : TabBaseUI
{
    [SerializeField] private List<Ability> _abilities;
    [SerializeField] private AbilityUI _abilityUIPrefab;
    [SerializeField] private Transform _abilityUIContainer;

    private void Start()
    {
        foreach (var ability in _abilities)
        {
            var newAbilityButton = Instantiate(_abilityUIPrefab, _abilityUIContainer);
            newAbilityButton.Bind(ability);
        }
    }
}
