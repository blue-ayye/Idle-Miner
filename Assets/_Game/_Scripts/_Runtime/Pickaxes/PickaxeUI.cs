using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickaxeUI : MonoBehaviour
{
    [SerializeField] private Image _displayImage;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private TMP_Text _upgradeButtonText;
    [SerializeField] private Button _upgradeButton;

    private PickaxeSO _pickaxe;

    public void Bind(PickaxeSO pickaxe)
    {
        _pickaxe = pickaxe;

        _pickaxe.OnUpgrade += UpdateVisuals;
        GameManager.OnGoldChanged += UpdateVisuals;

        UpdateVisuals();
    }

    private void OnDestroy()
    {
        if (_pickaxe) _pickaxe.OnUpgrade -= UpdateVisuals;
        GameManager.OnGoldChanged -= UpdateVisuals;
    }

    public void Upgrade()
    {
        _pickaxe.Upgrade();
    }

    private void UpdateVisuals()
    {
        if (!_pickaxe) return;

        _displayImage.sprite = _pickaxe.DisplayIcon;
        _titleText.SetText(_pickaxe.DisplayName);
        _levelText.SetText($"{_pickaxe.Level}");
        _descriptionText.SetText(_pickaxe.Description);

        var upgrades = _pickaxe.GetAvailableUpgrades();
        _costText.SetText($"{upgrades.cost}");
        var upgradeText = upgrades.upgradeTimes > 1 ? $"Upgrade X{upgrades.upgradeTimes}" : $"Upgrade";
        _upgradeButtonText.SetText(upgradeText);
        _upgradeButton.interactable = upgrades.canAfford;
    }
}