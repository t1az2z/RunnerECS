using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class ObstacleView : MonoBehaviour
    {
        public EcsEntity Entity;

        private void OnTriggerEnter(Collider other)
        {
            if (Entity.IsNull())
            {
                Debug.LogError("Entity is null on coin");
                return;
            }
            Entity.Get<CollisionEvent>().Type = CollisionType.Obstacle;
        }
    }
}