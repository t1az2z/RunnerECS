using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public  class SpawnCoinsSystem : IEcsRunSystem
    {
        private EcsWorld _world = null;
        private Configuration _configuration = null;
        private GameState _gameState = null;
        private SceneData _sceneData = null;
        private EcsFilter<SpawnCoinEvent, SpawnLaneIndexComponent> _filter = null;
        public void Run()
        {
            if (_gameState.State != State.Game || _filter.IsEmpty())
                return;

            foreach( int index in _filter)
            {
                var laneIndex = _filter.Get2(index).Value;
                var lanePosition = _configuration.LanesPositions[laneIndex];
                Vector3 position = new Vector3(lanePosition.x, lanePosition.y, _configuration.SpawnDistance);
                var coinEntity = _world.NewEntity();
                CoinView coinView = _sceneData.CoinsPool.Get(_configuration.CoinPrefab);
                coinEntity.Get<CoinViewRefComponent>().Value = coinView;
                coinView.transform.position = position;
                coinView.Entity = coinEntity;
                coinEntity.Get<WorldObjectComponent>().Transform = coinView.transform;
                ref var moveComponent = ref coinEntity.Get<MoveComponent>();
                moveComponent.Direction = Vector3.back;
                //TODO add coins speed increment
                moveComponent.Speed = _configuration.MovementSpeed;
                ref var spawnCoinEntity = ref _filter.GetEntity(index);
                spawnCoinEntity.Get<CoinSpawnCooldownComponent>().Value = _configuration.CoinSpawnCooldown;
            }
        }
    }
}