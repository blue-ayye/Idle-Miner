using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private Image _displayImage;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _costText;

    private Ability _ability;

    internal void Bind(Ability ability)
    {
        _ability = ability;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        _displayImage.sprite = _ability.DisplayIcon;
        _titleText.SetText(_ability.Title);
        _levelText.SetText($"{_ability.Level}");
        _descriptionText.SetText(_ability.Description);
        _costText.SetText($"{_ability.Cost}");
    }
}