using DG.Tweening;
using RunnerTT;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameScreen : UIScreen
{
    [SerializeField] private Image coinImage;
    [SerializeField] private TMP_Text coinsCounter;
    [SerializeField] private TMP_Text distanceCounter;

    private void Start()
    {
        GameState.OnCoinsChange += OnCoinsChange;
        GameState.OnDistanceChange += OnDistanceChange;
    }
    private void OnCoinsChange(int coins)
    {
        coinsCounter.text = $"Coins: {coins}";
        coinImage.rectTransform.DOPunchScale(Vector3.one*.4f, .1f, 2);
        coinImage.rectTransform.localScale = Vector3.ClampMagnitude(coinImage.rectTransform.localScale, 1.5f);
    }
    private void OnDistanceChange(float distance)
    {
        distanceCounter.text = $"Distance: {distance : 0.0}";
    }
    private void OnDestroy()
    {
        GameState.OnCoinsChange -= OnCoinsChange;
        GameState.OnDistanceChange -= OnDistanceChange;
    }
}
