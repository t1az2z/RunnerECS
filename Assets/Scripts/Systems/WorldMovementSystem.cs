using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    internal class WorldMovementSystem : IEcsRunSystem
    {
        private Configuration _configuration = null;
        private EcsFilter<WorldObject, Move> _filter = null;
        private GameState _gameState = null;
        public void Run()
        {
            if (_gameState.State == State.Game)
            {
                var deltaTime = Time.deltaTime;
                foreach (var index in _filter)
                {
                    var transform = _filter.Get1(index).Transform;
                    var direction = _filter.Get2(index).Direction;
                    var speed = _filter.Get2(index).Speed;
                    speed += _configuration.SpeedUpPerCoin * _gameState.CoinsCount;
                    transform.Translate(direction * speed * deltaTime);
                }
            }
        }
    }
}