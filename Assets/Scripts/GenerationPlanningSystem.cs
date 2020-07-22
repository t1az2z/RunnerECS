using Leopotam.Ecs;
using System.IO;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

namespace RunnerTT
{
    public class GenerationPlanningSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private Configuration _configuration;
        private GameState GameState;
        private EcsFilter<SpawnLaneIndexComponent, TimeSinceObsacleSpawnComponent, TimeTillNextSpawnComponent, CoinSpawnCooldownComponent> _filter;
        public void Run()
        {
            if (_filter.IsEmpty())
            {
                return;
            }

            foreach (var index in _filter)
            {
                if (GameState.State != State.Game)
                    return;

                ref var timeSinceLastSpawnComponent = ref _filter.Get2(index);
                timeSinceLastSpawnComponent.Value += Time.deltaTime;

                ref var coinCooldown = ref _filter.Get4(index);
                coinCooldown.Value -= Time.deltaTime;

                var timeTillNextSpawn = _filter.Get3(index).Value;

                ref var entity = ref _filter.GetEntity(index);

                //var obstacleCanBeSpawned = false;
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
                    if (Random.Range(0, 100) <_configuration.CoinGenerationChance)
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

        //private bool CoinCanBeSpawned(EcsEntity entity, bool obstacleCanBeSpawned)
        //{
        //    bool coinCanBeSpawned = false;
        //    var timeSinceLastObstacleSpawn = entity.Get<TimeSinceObsacleSpawnComponent>().Value;
        //    var timeTillNextObstacleSpawn = entity.Get<TimeTillNextSpawnComponent>().Value;

        //    bool cooldownEnded = entity.Get<CoinSpawnCooldownComponent>().Value <= 0;
        //    bool passEnoughTimeSinceLastSpawn = timeSinceLastObstacleSpawn >= _configuration.CoinSpawnCooldown;
        //    bool haveEnoughTimeTillNextSpawn = timeTillNextObstacleSpawn-timeSinceLastObstacleSpawn >= _configuration.CoinSpawnCooldown || timeTillNextObstacleSpawn - timeSinceLastObstacleSpawn < 0;
        //    if (entity.Get<SpawnLaneIndexComponent>().Value == 1)
        //    {
        //        Debug.Log($"passenoughtime {passEnoughTimeSinceLastSpawn}, haveEnoughTime {timeTillNextObstacleSpawn - timeSinceLastObstacleSpawn}. coooldown {cooldownEnded}. hettns {haveEnoughTimeTillNextSpawn} ttnos {timeTillNextObstacleSpawn}");
        //    }

        //    if (cooldownEnded && (!obstacleCanBeSpawned && haveEnoughTimeTillNextSpawn) && passEnoughTimeSinceLastSpawn)
        //        coinCanBeSpawned = true;


        //    return coinCanBeSpawned;
        //}
    }
}