using RunnerTT;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RunnerTT
{
    public class EndGameScreen : UIScreen
    {
        [SerializeField] private TMP_Text coinsCounter;
        [SerializeField] private TMP_Text distanceCounter;

        private void Start()
        {
            GameState.OnCoinsChange += OnCoinsChange;
            GameState.OnDistanceChange += OnDistanceChange;
        }
        private void OnCoinsChange(int coins)
        {
            coinsCounter.text = $"Your coins: {coins}";
        }
        private void OnDistanceChange(float distance)
        {
            distanceCounter.text = $"Your distance: {distance: 0.0}";
        }
        private void OnDestroy()
        {
            GameState.OnCoinsChange -= OnCoinsChange;
            GameState.OnDistanceChange -= OnDistanceChange;
        }
    }
}
