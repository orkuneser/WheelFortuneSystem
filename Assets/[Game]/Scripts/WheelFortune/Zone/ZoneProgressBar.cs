using UnityEngine;
using Sirenix.OdinInspector;

public class ZoneProgressBar : MonoBehaviour
{
    [Title("Refs")]
    [SerializeField] private Transform _container;
    [SerializeField] private ZoneProgressBarItem _itemPrefab;

    [Title("Settings")]
    [Min(1)]
    [SerializeField] private int _zoneCount = 10;

    private void Start()
    {
        BuildBar();
    }

    private void BuildBar()
    {
        if (_container == null || _itemPrefab == null)
            return;

        for (int i = _container.childCount - 1; i >= 0; i--)
        {
            var child = _container.GetChild(i);
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
#else
            Destroy(child.gameObject);
#endif
        }

        for (int zoneNumber = 1; zoneNumber <= _zoneCount; zoneNumber++)
        {
            var item = Instantiate(_itemPrefab, _container);
            item.Setup(zoneNumber);
        }
    }
}
