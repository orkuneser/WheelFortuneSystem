using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;

public class RewardFlowManager : BaseEventListener<RewardEarnedEvent>
{
    [Title("Settings")]
    [SerializeField] private int _itemSpawnCount = 5;
    [SerializeField] private float _scatterRadius = 200f;
    [SerializeField] private float _scatterDuration = 0.5f;
    [SerializeField] private float _flyDuration = 0.8f;

    [Title("Timing")]
    [SerializeField] private float _delayAfterScatter = 0.1f;
    [SerializeField] private float _delayBeforeFinish = 0.2f;

    [Title("References")]
    [SerializeField] private GameObject _flyingIconPrefab;
    [SerializeField] private Transform _spawnCenter;
    [SerializeField] private Transform _targetPanelContainer;

    protected override void OnEvent(RewardEarnedEvent evt)
    {
        if (evt.Config.Type == RewardType.Bomb) return;

        StartCoroutine(PlayRewardRoutine(evt));
    }

    private IEnumerator PlayRewardRoutine(RewardEarnedEvent evt)
    {
        List<GameObject> activeIcons = new List<GameObject>();

        for (int i = 0; i < _itemSpawnCount; i++)
        {
            GameObject iconObj = Instantiate(_flyingIconPrefab, _spawnCenter.parent);
            iconObj.transform.position = _spawnCenter.position;

            var img = iconObj.GetComponent<UnityEngine.UI.Image>();
            if (img != null) img.sprite = evt.Config.Icon;

            activeIcons.Add(iconObj);

            Vector3 randomPos = _spawnCenter.position + (Vector3)Random.insideUnitCircle * _scatterRadius;
            iconObj.transform.DOMove(randomPos, _scatterDuration).SetEase(Ease.OutBack);
        }

        yield return new WaitForSeconds(_scatterDuration + _delayAfterScatter);

        foreach (var icon in activeIcons)
        {
            icon.transform.DOMove(_targetPanelContainer.position, _flyDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => Destroy(icon));

            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(_flyDuration);
        EventManager.Raise(new RewardsListUpdatedEvent());
        yield return new WaitForSeconds(_delayBeforeFinish);
        EventManager.Raise(new RewardAnimationCompletedEvent());
    }
}