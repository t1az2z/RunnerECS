using Leopotam.Ecs;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RunnerTT
{
    public class DistanceCountSystem : IEcsRunSystem
    {
        private GameState _gameState;
        private EcsFilter<DistanceCounterComponent, MoveComponent, WorldObjectComponent> _filter;

        public void Run()
        {
            if (_filter.IsEmpty() || _gameState.State != State.Game)
                return;

            ref var moveComponent = ref _filter.Get2(0);
            moveComponent.Speed = _gameState.CurrentSpeed;
            var position = _filter.Get3(0).Transform.position;
            var distance = Vector3.Distance(position, Vector3.zero);
            _gameState.CurrentDistance = distance;
        }
    }
}