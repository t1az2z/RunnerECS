using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunnerTT
{
    public class InputProcessingSystem : IEcsRunSystem
    {
        private GameState _gameState;
        private Configuration _configuration;
        private EcsFilter<InputEvet> _filter;
        private SceneData _sceneData;
        private EcsFilter<CurrentLaneComponent, MoveComponent> _player;
        public void Run()
        {
            foreach(var index in _filter)
            {
                var input = _filter.Get1(index).Value;
                var currentLane = _player.Get1(index).Value;
                var maxLaneIndex= _configuration.LanesPositions.Length-1;
                switch(input)
                {
                    case ButtonPress.Left:
                        if (_player.IsEmpty())
                            return;
                        _player.Get1(0).Value = Mathf.Clamp(currentLane - 1, 0, maxLaneIndex);
                        break;

                    case ButtonPress.Right:
                        if (_player.IsEmpty())
                            return;
                        _player.Get1(0).Value = Mathf.Clamp(currentLane+1, 0, maxLaneIndex);
                        break;

                    case ButtonPress.Space:
                        if (_gameState.State == State.Game)
                            return;

                        if (_gameState.State == State.Start)
                            _gameState.State = State.Game;
                        else if (_gameState.State == State.End)
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        break;
                }
            }
        }
    }
}