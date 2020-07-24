using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class PlayerDeathProcessingSystem : IEcsRunSystem
    {
        private Configuration _configuration = null;
        private GameState _gameState = null;
        private EcsFilter<PlayerViewRefComponent, PlayerDeathEvent> _player = null;

        public void Run()
        {
            if (!_player.IsEmpty())
            {
                _gameState.State = State.Death;
                ref var player = ref _player.Get1(0);
                player.Value.StartCoroutine(player.Value.EndGameAfterDeathAnimation(_configuration.DeathAnimationTime, _gameState));
            }
            if(_gameState.State == State.End)
            {
                PlayerPrefs.SetInt("CoinsCount", _gameState.CoinsCount);
            }
        }

        
    }
}