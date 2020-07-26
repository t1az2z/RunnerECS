using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class SpawnPointsInitializationSystem : IEcsInitSystem
    {
        private Configuration _configuration = null;
        private EcsWorld _world = null;

        public void Init()
        {
            for (int i = 0; i < _configuration.LanesPositions.Length; i++)
            {
                var currentLaneIndex = i;
                var entity = _world.NewEntity();
                entity.Get<SpawnLaneIndexComponent>().Value = currentLaneIndex;
                entity.Get<TimeSinceObsacleSpawnComponent>().Value = 0;
                entity.Get<TimeTillNextSpawnComponent>().Value = Random.Range(_configuration.ObstacleMinSpawnTime, _configuration.ObstacleMaxSpawnTime);
                entity.Get<CoinSpawnCooldownComponent>().Value = _configuration.CoinSpawnCooldown;
            }
        }
    }
}