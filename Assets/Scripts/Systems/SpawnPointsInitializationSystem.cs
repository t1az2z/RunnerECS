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
                entity.Get<SpawnLaneIndex>().Value = currentLaneIndex;
                entity.Get<TimeSinceObsacleSpawn>().Value = 0;
                entity.Get<TimeTillNextSpawn>().Value = Random.Range(_configuration.ObstacleMinSpawnTime, _configuration.ObstacleMaxSpawnTime);
                entity.Get<CoinSpawnCooldown>().Value = _configuration.CoinSpawnCooldown;
            }
        }
    }
}