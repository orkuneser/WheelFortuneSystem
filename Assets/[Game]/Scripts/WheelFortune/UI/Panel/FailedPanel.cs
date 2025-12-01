using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class FailedPanel : UiFadePanel
{
    [Title("Buttons")]
    [SerializeField] private Button _giveUpButton;
    [SerializeField] private Button _reviveGoldButton;
    [SerializeField] private Button _reviveAdsButton;

    [Title("Settings")]
    [SerializeField] private int _reviveCost = 25;

    private void OnEnable()
    {
        EventManager.Add<BombHitEvent>(OnBombHit);

        _giveUpButton.onClick.AddListener(OnClick_GiveUp);
        _reviveGoldButton.onClick.AddListener(OnClick_ReviveGold);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        EventManager.Remove<BombHitEvent>(OnBombHit);

        _giveUpButton.onClick.RemoveListener(OnClick_GiveUp);
        _reviveGoldButton.onClick.RemoveListener(OnClick_ReviveGold);
    }

    private void OnBombHit(BombHitEvent evt)
    {
        ShowPanelAnimated();
    }

    private void OnClick_GiveUp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnClick_ReviveGold()
    {
        if (GoldManager.Instance.Spend(_reviveCost))
        {
            HidePanelAnimated();
            ZoneController.Instance.NextZone();
        }
        else
            Debug.Log("No Enough Gold!");
    }
}