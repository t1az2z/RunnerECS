using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private Configuration _configuration = null;
        private EcsFilter<CurrentLane, WorldObject> _filter = null;
        public void Run()
        {
            var deltaTime = Time.deltaTime;

            foreach(var index in _filter)
            {
                var lanePositionIndex = _filter.Get1(index).Value;
                var lanePosition = _configuration.LanesPositions[lanePositionIndex];
                var transform = _filter.Get2(index).Transform;
                var moveSpeed = _configuration.MovementSpeed;
                if (transform.position != lanePosition)
                {
                    transform.position = Vector3.Lerp(transform.position, lanePosition, moveSpeed * deltaTime);
                }
            }
        }
    }
}