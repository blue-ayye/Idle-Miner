using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private float _price;
    [SerializeField] private float _priceMultiplier;
    [SerializeField, Multiline] private string _description;

    public Sprite DisplayIcon => _iconSprite;

    public string Title => _name;

    public int Level => _level;

    public string Description => _description;

    public float Cost => _price; //unlock cost = level 1 price
}