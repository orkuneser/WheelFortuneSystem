using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;

public class RewardFlowManager : BaseEventListener<RewardEarnedEvent>
{
    [Title("UI References")]
    [SerializeField] private UiRewardCardPanel _cardPanel;
    [SerializeField] private Transform _targetPanelContainer;

    [Title("Animation Prefabs")]
    [SerializeField] private GameObject _flyingIconPrefab;

    [Title("Settings")]
    [SerializeField] private int _itemSpawnCount = 5;
    [SerializeField] private float _scatterRadius = 75f;
    [SerializeField] private float _scatterDuration = 0.5f;
    [SerializeField] private float _flyDuration = 0.8f;

    [Title("Timing")]
    [SerializeField] private float _waitDurationOnCard = 0.5f;
    [SerializeField] private float _delayBeforeFinish = 0.2f;

    protected override void OnEvent(RewardEarnedEvent evt)
    {
        if (evt.Config.Type == RewardType.Bomb) return;

        StartCoroutine(PlayRewardSequence(evt));
    }

    private IEnumerator PlayRewardSequence(RewardEarnedEvent evt)
    {
        _cardPanel.Setup(evt.Config, evt.Amount);
        _cardPanel.Show();

        yield return new WaitForSeconds(0.6f + _waitDurationOnCard);

        Vector3 spawnOrigin = _cardPanel.IconTransform.position;
        List<GameObject> activeIcons = new List<GameObject>();

        for (int i = 0; i < _itemSpawnCount; i++)
        {
            GameObject iconObj = Instantiate(_flyingIconPrefab, _cardPanel.transform.parent);
            iconObj.transform.position = spawnOrigin;
            iconObj.transform.localScale = Vector3.one * 0.5f;

            var img = iconObj.GetComponent<UnityEngine.UI.Image>();
            if (img != null) img.sprite = evt.Config.Icon;

            activeIcons.Add(iconObj);

            Vector3 randomPos = spawnOrigin + (Vector3)Random.insideUnitCircle * _scatterRadius;
            iconObj.transform.DOMove(randomPos, _scatterDuration).SetEase(Ease.OutBack);
        }

        yield return new WaitForSeconds(_scatterDuration);

        foreach (var icon in activeIcons)
        {
            icon.transform.DOMove(_targetPanelContainer.position, _flyDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(icon));

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(_flyDuration);

        EventManager.Raise(new RewardsListUpdatedEvent());

        _cardPanel.Hide();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(_delayBeforeFinish);
        EventManager.Raise(new RewardAnimationCompletedEvent());
    }
}