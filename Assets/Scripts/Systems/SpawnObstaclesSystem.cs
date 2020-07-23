using Leopotam.Ecs;
using TMPro.EditorUtilities;
using UnityEngine;

namespace RunnerTT
{
    public class SpawnObstaclesSystem : IEcsRunSystem
    {
        private Configuration _configuration = null;
        private EcsWorld _world = null;
        private GameState _gameState = null;
        private SceneData _sceneData = null;
        private EcsFilter<SpawnObstacleEvent, SpawnLaneIndexComponent, TimeSinceObsacleSpawnComponent, TimeTillNextSpawnComponent> _filter = null;

        public void Run()
        {
            if (_gameState.State != State.Game || _filter.IsEmpty())
                return;

            foreach (var index in _filter)
            {
                var laneindex = _filter.Get2(index).Value;
                var lanePosition = _configuration.LanesPositions[laneindex];
                Vector3 position = new Vector3(lanePosition.x, lanePosition.y, _configuration.SpawnDistance);
                var obstacleEntity = _world.NewEntity();
                ObstacleView obstacleView = _sceneData.ObsaclesPool.Get(_configuration.ObstaclePrefab);
                obstacleView.transform.position = position;
                obstacleView.Entity = obstacleEntity;
                obstacleEntity.Get<WorldObjectComponent>().Transform = obstacleView.transform;
                obstacleEntity.Get<ObstacleViewRefComponent>().Value = obstacleView;
                ref var moveComponent = ref obstacleEntity.Get<MoveComponent>();
                moveComponent.Direction = Vector3.back;
                moveComponent.Speed = _configuration.MovementSpeed;
                ref var spawnObstacleEntity = ref _filter.GetEntity(index);
                ResetSpawnEntity(spawnObstacleEntity);
            }
        }

        private void ResetSpawnEntity(in EcsEntity entity)
        {
            entity.Get<TimeSinceObsacleSpawnComponent>().Value = 0;
            entity.Get<TimeTillNextSpawnComponent>().Value = _configuration.RandomTimeForSpawn;
        }
    }
}