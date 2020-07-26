using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class ObstaclesSpawnSystem : IEcsRunSystem
    {
        private Configuration _configuration = null;
        private EcsWorld _world = null;
        private GameState _gameState = null;
        private SceneData _sceneData = null;
        private EcsFilter<SpawnObstacleEvent, SpawnLaneIndex, TimeSinceObsacleSpawn, TimeTillNextSpawn> _filter = null;

        public void Run()
        {
            if (_gameState.State == State.Game)
            {
                foreach (var index in _filter)
                {
                    var laneindex = _filter.Get2(index).Value;
                    var lanePosition = _configuration.LanesPositions[laneindex];
                    Vector3 position = new Vector3(lanePosition.x, lanePosition.y, _configuration.SpawnDistance);
                    var obstacleEntity = _world.NewEntity();
                    ObstacleView obstacleView = _sceneData.ObsaclesPool.Get(_configuration.ObstaclePrefab);
                    obstacleView.transform.position = position;
                    obstacleView.Entity = obstacleEntity;
                    obstacleEntity.Get<WorldObject>().Transform = obstacleView.transform;
                    obstacleEntity.Get<ObstacleViewRef>().Value = obstacleView;
                    ref var moveComponent = ref obstacleEntity.Get<Move>();
                    moveComponent.Direction = Vector3.back;
                    moveComponent.Speed = _configuration.MovementSpeed;
                    ref var spawnObstacleEntity = ref _filter.GetEntity(index);
                    ResetSpawnEntity(spawnObstacleEntity);
                    obstacleView.gameObject.SetActive(true);
                }
            }
        }

        private void ResetSpawnEntity(in EcsEntity entity)
        {
            entity.Get<TimeSinceObsacleSpawn>().Value = 0;
            entity.Get<TimeTillNextSpawn>().Value = Random.Range(_configuration.ObstacleMinSpawnTime, _configuration.ObstacleMaxSpawnTime);
        }
    }
}