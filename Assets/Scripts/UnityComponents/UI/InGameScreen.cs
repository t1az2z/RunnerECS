using RunnerTT;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameScreen : UIScreen
{
    [SerializeField] private Image coinImage;
    [SerializeField] private TMP_Text coinsCounter;
    [SerializeField] private TMP_Text distanceCounter;

    private void Start()
    {
        base.OpenScreen();
        GameState.OnCoinsChange += OnCoinsChange;
        GameState.OnDistanceChange += OnDistanceChange;
    }
    private void OnCoinsChange(int coins)
    {
        coinsCounter.text = $"Coins: {coins}";
        coinImage.rectTransform.DOPunchAnchorPos(Vector2.up, .5f);
    }
    private void OnDistanceChange(float distance)
    {
        distanceCounter.text = $"Distance: {distance : 0.0}";
    }
}
