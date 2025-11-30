using UnityEngine;
using Sirenix.OdinInspector;

public class GoldManager : Singleton<GoldManager>
{
    [Title("Started Gold Amount")]
    [SerializeField] private int _defaultGold = 0;

    private int _goldAmount;
    public int GoldAmount
    {
        get => _goldAmount;
        set
        {
            _goldAmount = value;
            Save();
            EventManager.Raise(new GoldChangedEvent(_goldAmount));
        }
    }

    private void Awake()
    {
        LoadGoldAmount();
    }

    private void Start()
    {
        EventManager.Raise(new GoldChangedEvent(_goldAmount));
    }

    private void LoadGoldAmount()
    {
        _goldAmount = PlayerPrefs.GetInt(PlayerPrefsKeys.GOLDAMOUNTKEY, _defaultGold);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.GOLDAMOUNTKEY, _goldAmount);
        PlayerPrefs.Save();
    }

    [Button]
    public void Add(int amount)
    {
        if (amount <= 0) return;
        _goldAmount += amount;
    }

    [Button]
    public bool Spend(int amount)
    {
        if (amount <= 0) return false;
        if (_goldAmount < amount) return false;

        GoldAmount -= amount;
        return true;
    }
}
