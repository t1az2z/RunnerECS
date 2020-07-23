using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class GenerationPlanningSystem : IEcsRunSystem
    {
        private Configuration _configuration = null;
        private GameState _gameState = null;
        private EcsFilter<SpawnLaneIndexComponent, TimeSinceObsacleSpawnComponent, TimeTillNextSpawnComponent, CoinSpawnCooldownComponent> _filter = null;
        public void Run()
        {
            if (_filter.IsEmpty())
            {
                return;
            }

            foreach (var index in _filter)
            {
                if (_gameState.State != State.Game)
                    return;

                ref var timeSinceLastSpawnComponent = ref _filter.Get2(index);
                timeSinceLastSpawnComponent.Value += Time.deltaTime;

                ref var coinCooldown = ref _filter.Get4(index);
                var generationSpeedCompensation = _gameState.CoinsCount * _configuration.MaxSpeedUpCoins;
                coinCooldown.Value -= Time.deltaTime+generationSpeedCompensation*.01f;

                var timeTillNextSpawn = _filter.Get3(index).Value;

                ref var entity = ref _filter.GetEntity(index);

                if (timeSinceLastSpawnComponent.Value >= timeTillNextSpawn)
                {
                    var obstacleCanBeSpawned = ObstacleCanBeSpawned(_filter, _configuration.RandomTimeForSpawn);
                    if (obstacleCanBeSpawned)
                    {
                        entity.Get<SpawnObstacleEvent>();
                    }
                }
                bool coinCanBeSpawned = _filter.Get4(index).Value <= 0;
                if (coinCanBeSpawned)
                {
                    if (Random.Range(0, 100) < _configuration.CoinGenerationChance)
                        entity.Get<SpawnCoinEvent>();
                }
            }
        }

        private bool ObstacleCanBeSpawned(EcsFilter<SpawnLaneIndexComponent, TimeSinceObsacleSpawnComponent, TimeTillNextSpawnComponent, CoinSpawnCooldownComponent> filter, float randomSpawnTime)
        {
            int trueCounter = 0;
            bool canBeSpawned = false;
            foreach (var i in filter)
            {
                var timeSinceLastSpawn = filter.Get2(i).Value;

                if (timeSinceLastSpawn > randomSpawnTime)
                    trueCounter++;
            }
            if (trueCounter == filter.GetEntitiesCount())
                canBeSpawned = true;

            return canBeSpawned;
        }
    }
}