using System;
using UnityEngine;

namespace RunnerTT
{
    public class GameState
    {
        public static Action<State> OnGameStateChange;
        public static Action<float> OnDistanceChange;
        public static Action<int> OnCoinsChange;

        public float CurrentSpeed;

        private State _state;
        public State State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnGameStateChange?.Invoke(_state);
            }
        }

        private float _currentDistance;
        public float CurrentDistance
        {
            get
            {
                return _currentDistance;
            }
            set
            {
                _currentDistance = value;
                OnDistanceChange?.Invoke(_currentDistance);
            }
        }

        private int _coinsCount;
        public int CoinsCount
        {
            get
            {
                return _coinsCount;
            }
            set
            {
                _coinsCount = value;
                OnCoinsChange?.Invoke(_coinsCount);
            }
        }

        public GameState(float currentSpeed)
        {
            CurrentSpeed = currentSpeed;
            CurrentDistance = 0;
            CoinsCount = 0;
            if (PlayerPrefs.HasKey("CoinsCount"))
                CoinsCount = PlayerPrefs.GetInt("CoinsCount");
        }
    }
    public enum State
    {
        Start,
        End,
        Death,
        Game
    }
}