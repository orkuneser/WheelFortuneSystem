using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class ZoneProgressBar : BaseEventListener<ZoneChangedEvent>
{
    [Title("Refs")]
    [SerializeField] private Transform _container;
    [SerializeField] private ZoneProgressBarItem _itemPrefab;

    [Title("Settings")]
    [Min(1)]
    [SerializeField] private int _zoneCount = 120;

    [Title("Move Settings")]
    [SerializeField] private float _moveOffset = 100f;
    [SerializeField] private float _moveDuration = 0.3f;
    [SerializeField] private Ease _moveEase = Ease.OutQuad;

    private float _initialX;

    private void Start()
    {
        BuildBar();

        _initialX = _container.localPosition.x;
        MoveToZone(ZoneController.Instance.CurrentZone, instant: true);
    }

    private void BuildBar()
    {
        if (_container == null || _itemPrefab == null)
            return;

        for (int zoneNumber = 1; zoneNumber <= _zoneCount; zoneNumber++)
        {
            var item = Instantiate(_itemPrefab, _container);
            item.Setup(zoneNumber);
        }
    }

    protected override void OnEvent(ZoneChangedEvent evt)
    {
        MoveToZone(evt.Zone, instant: false);
    }

    private void MoveToZone(int zone, bool instant)
    {
        float targetX = _initialX - (zone - 1) * _moveOffset;
        var current = _container.localPosition;

        if (instant)
            _container.localPosition = new Vector3(targetX, current.y, current.z);
        else
            _container.DOLocalMoveX(targetX, _moveDuration)
                      .SetEase(_moveEase);
    }
}
