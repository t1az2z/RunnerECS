using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private Configuration _configuration;
        private EcsFilter<CurrentLaneComponent, WorldObjectComponent> _filter;
        public void Run()
        {
            if (_filter.IsEmpty())
                return;

            foreach(var index in _filter)
            {
                var lanePositionIndex = _filter.Get1(index).Value;
                var lanePosition = _configuration.LanesPositions[lanePositionIndex];
                var transform = _filter.Get2(index).Transform;
                var moveSpeed = _configuration.MovementSpeed;
                if (transform.position != lanePosition)
                {
                    transform.position = Vector3.Lerp(transform.position, lanePosition, moveSpeed * Time.deltaTime);
                }
            }
        }
    }
}