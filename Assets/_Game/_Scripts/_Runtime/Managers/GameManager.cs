using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action OnGoldChanged;

    [SerializeField] private float _gold;

    [Header("Debug")]
    [SerializeField] private bool _debug = false;

    [SerializeField] private float _addGold = 500;
    [SerializeField] private float _ticks = 1f;

    public float Gold => _gold;

    public UpgradeTimes UpgradeTimes;

    private void Awake()
    {
        Instance = this;
    }

    private float timer;

    private void Update()
    {
        if (!_debug) return;
        if (Time.time > timer)
        {
            timer = Time.time + _ticks;
            AddGold(_addGold);
        }
    }

    public void AddGold(float gold) => UpdateGold(gold);

    public void RemoveGold(float gold) => UpdateGold(-gold);

    private void UpdateGold(float gold)
    {
        _gold += gold;
        OnGoldChanged?.Invoke();
    }
}

public enum UpgradeTimes
{
    X1 = 1,
    X10 = 10,
    X50 = 50,
    X100 = 100
}