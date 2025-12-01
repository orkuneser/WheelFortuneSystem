using UnityEngine;
using System.Collections.Generic;

public class UiCollectedPanel : BaseEventListener<RewardsListUpdatedEvent>
{
    [SerializeField] private Transform _contentContainer;
    [SerializeField] private UiCollectedItem _itemPrefab;

    private List<UiCollectedItem> _spawnedItems = new List<UiCollectedItem>();

    protected override void OnEvent(RewardsListUpdatedEvent evt)
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (var item in _spawnedItems) Destroy(item.gameObject);
        _spawnedItems.Clear();

        var rewards = RewardCollectionManager.Instance.CollectedRewards;

        foreach (var reward in rewards)
        {
            var uiItem = Instantiate(_itemPrefab, _contentContainer);
            uiItem.Setup(reward.Config, reward.TotalAmount);
            _spawnedItems.Add(uiItem);
        }
    }
}