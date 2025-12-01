using System.Collections.Generic;
using Sirenix.OdinInspector;

[System.Serializable]
public class CollectedReward
{
    public SpinSlotItemConfig Config;
    public int TotalAmount;
}

public class RewardCollectionManager : Singleton<RewardCollectionManager>
{
    [ShowInInspector, ReadOnly]
    private List<CollectedReward> _collectedRewards = new List<CollectedReward>();

    public List<CollectedReward> CollectedRewards => _collectedRewards;

    private void OnEnable()
    {
        EventManager.Add<RewardEarnedEvent>(OnRewardEarned);
    }

    private void OnDisable()
    {
        EventManager.Remove<RewardEarnedEvent>(OnRewardEarned);
    }

    private void OnRewardEarned(RewardEarnedEvent evt)
    {
        if (evt.Config.Type == RewardType.Bomb)
            return;

        AddToCollectionList(evt.Config, evt.Amount);
    }

    private void AddToCollectionList(SpinSlotItemConfig config, int amount)
    {
        var existing = _collectedRewards.Find(x => x.Config == config);
        if (existing != null)
        {
            existing.TotalAmount += amount;
        }
        else
        {
            _collectedRewards.Add(new CollectedReward
            {
                Config = config,
                TotalAmount = amount
            });
        }
    }

    public void ClearRewards()
    {
        _collectedRewards.Clear();
        EventManager.Raise(new RewardsListUpdatedEvent());
    }

    public void CollectToGlobalWallet()
    {
        foreach (var reward in _collectedRewards)
        {
            if (reward.Config.Type == RewardType.Currency)
            {
                GoldManager.Instance.Add(reward.TotalAmount);
            }
        }
        ClearRewards();
    }
}