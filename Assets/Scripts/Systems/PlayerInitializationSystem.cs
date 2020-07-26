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
            ref var moveComponent = ref playerEntity.Get<MoveComponent>();
            moveComponent.Direction = Vector3.zero;
            moveComponent.Speed = _configuration.MovementSpeed;
            playerEntity.Get<CurrentLaneComponent>().Value = _configuration.StartLaneIndex;
            playerEntity.Get<WorldObjectComponent>().Transform = _sceneData.PlayerView.transform;
            playerEntity.Get<PlayerViewRefComponent>().Value = _sceneData.PlayerView;
            _sceneData.PlayerView.Entity = playerEntity;
        }
    }
}