using Leopotam.Ecs;

namespace RunnerTT
{
    public class SpawnPointsInitializationSystem : IEcsInitSystem
    {
        private Configuration _configuration;
        private EcsWorld _world;

        public void Init()
        {
            for (int i = 0; i < _configuration.LanesPositions.Length; i++)
            {
                var currentLaneIndex = i;
                var entity = _world.NewEntity();
                entity.Get<SpawnLaneIndexComponent>().Value = currentLaneIndex;
                entity.Get<TimeSinceObsacleSpawnComponent>().Value = 0;
                entity.Get<TimeTillNextSpawnComponent>().Value = _configuration.RandomTimeForSpawn;
                entity.Get<CoinSpawnCooldownComponent>().Value = _configuration.CoinSpawnCooldown;
            }
        }
    }
}