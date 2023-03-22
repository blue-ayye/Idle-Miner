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
    [SerializeField] private TMP_Text _upgradeButtonText;
    [SerializeField] private Button _upgradeButton;

    private Ability _ability;

    public void Bind(Ability ability)
    {
        _ability = ability;

        _ability.OnUpgrade += UpdateVisuals;
        GameManager.OnGoldChanged += UpdateVisuals;

        UpdateVisuals();
    }

    private void OnDestroy()
    {
        if (_ability) _ability.OnUpgrade -= UpdateVisuals;
        GameManager.OnGoldChanged -= UpdateVisuals;
    }

    public void Upgrade()
    {
        _ability.Upgrade();
    }

    private void UpdateVisuals()
    {
        if (!_ability) return;

        _displayImage.sprite = _ability.DisplayIcon;
        _titleText.SetText(_ability.Title);
        _levelText.SetText($"{_ability.Level}");
        _descriptionText.SetText(_ability.Description);

        var upgrade = _ability.GetAvailableUpgrades();
        _costText.SetText($"{upgrade.cost}");
        _upgradeButtonText.SetText($"Upgrade X{upgrade.upgrades}");
        _upgradeButton.interactable = upgrade.canAfford ? true : false;
    }
}