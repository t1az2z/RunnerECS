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
            foreach (var index in _filter)
            {
                if (_gameState.State != State.Game)
                    return;

                ref var timeSinceLastSpawnComponent = ref _filter.Get2(index);
                timeSinceLastSpawnComponent.Value += Time.deltaTime;

                ref var coinCooldown = ref _filter.Get4(index);
                var coinsSpeedCoef = Mathf.Clamp01(_gameState.CoinsCount * _configuration.SpeedUpPerCoin / _configuration.MaxSpeedUp);
                coinCooldown.Value = (coinCooldown.Value - (coinCooldown.Value * coinsSpeedCoef)) - Time.deltaTime;

                var timeTillNextSpawn = _filter.Get3(index).Value;

                ref var entity = ref _filter.GetEntity(index);

                if (timeSinceLastSpawnComponent.Value >= timeTillNextSpawn)
                {
                    float minSpawnTime = _configuration.ObstacleMinSpawnTime - (_configuration.ObstacleMinSpawnTime * coinsSpeedCoef);
                    float maxSpawnTime = _configuration.ObstacleMaxSpawnTime - (_configuration.ObstacleMaxSpawnTime * coinsSpeedCoef);
                    var randomTime = Random.Range(minSpawnTime, maxSpawnTime);
                    var obstacleCanBeSpawned = ObstacleCanBeSpawned(_filter, randomTime - (randomTime * coinsSpeedCoef));
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
            foreach (var index in filter)
            {
                var timeSinceLastSpawn = filter.Get2(index).Value;

                if (timeSinceLastSpawn > randomSpawnTime)
                    trueCounter++;
            }
            if (trueCounter == filter.GetEntitiesCount())
                canBeSpawned = true;

            return canBeSpawned;
        }
    }
}