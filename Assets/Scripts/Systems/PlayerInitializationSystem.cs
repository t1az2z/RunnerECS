using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class PlayerInitializationSystem : IEcsInitSystem
    {
        private Configuration _configuration = null;
        private SceneData _sceneData = null;
        private EcsWorld _world = null;

        public void Init()
        {
            var playerEntity = _world.NewEntity();
            ref var moveComponent = ref playerEntity.Get<Move>();
            moveComponent.Direction = Vector3.zero;
            moveComponent.Speed = _configuration.MovementSpeed;
            playerEntity.Get<CurrentLane>().Value = _configuration.StartLaneIndex;
            playerEntity.Get<WorldObject>().Transform = _sceneData.PlayerView.transform;
            playerEntity.Get<PlayerViewRef>().Value = _sceneData.PlayerView;
            _sceneData.PlayerView.Entity = playerEntity;
        }
    }
}