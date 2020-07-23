using System.Collections.Generic;
using UnityEngine;

namespace RunnerTT
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private StartScreen startScreen;
        [SerializeField] private InGameScreen inGameScreen;
        [SerializeField] private EndGameScreen endGameScree;

        public Dictionary<State, UIScreen> screens = new Dictionary<State, UIScreen>();
        private void Awake()
        {
            screens = new Dictionary<State, UIScreen>()
            {
                {State.Start, startScreen },
                {State.Game, inGameScreen },
                {State.End, endGameScree}
            };
        }
        public void OnGameStateChange(State state)
        {
            foreach (KeyValuePair<State, UIScreen> screen in screens)
            {
                if (screen.Key != state)
                    screen.Value.CloseScreen();
                else
                    screen.Value.OpenScreen();
            }
        }
    }
}