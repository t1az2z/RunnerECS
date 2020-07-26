using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class DistanceCountSystem : IEcsRunSystem
    {
        private GameState _gameState = null;
        private EcsFilter<DistanceCounter, Move, WorldObject> _filter = null;

        public void Run()
        {
            if (_gameState.State == State.Game)
            {
                foreach (int index in _filter)
                {
                    ref var moveComponent = ref _filter.Get2(index);
                    moveComponent.Speed = _gameState.CurrentSpeed;
                    var position = _filter.Get3(index).Transform.position;
                    var distance = Vector3.Distance(position, Vector3.zero);
                    _gameState.CurrentDistance = distance;
                    return;
                }
            }
        }
    }
}