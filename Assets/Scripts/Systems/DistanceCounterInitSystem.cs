using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class DistanceCounterInitSystem : IEcsInitSystem
    {
        private GameState _gameState = null;
        private EcsWorld _world = null;
        public void Init()
        {
            var entity = _world.NewEntity();
            var distanceCounterGO = new GameObject();
            distanceCounterGO.name = "DistanceCounter";
            distanceCounterGO.transform.position = Vector3.zero; 
            entity.Get<WorldObjectComponent>().Transform = distanceCounterGO.transform;
            ref var moveComponent = ref entity.Get<MoveComponent>();
            moveComponent.Direction = Vector3.back;
            moveComponent.Speed = _gameState.CurrentSpeed;
            entity.Get<DistanceCounterComponent>();
        }
    }
}