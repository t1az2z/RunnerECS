using Leopotam.Ecs;
using RunnerTT;

namespace RunnerTT
{
    public class ObstaclesPool : GenericObjectPool<ObstacleView>
    {
        public override void ReturnToPool(ObstacleView objectToReturn)
        {
            var entity = objectToReturn.Entity;
            entity.Del<WorldObject>();
            entity.Del<Move>();
            base.ReturnToPool(objectToReturn);
        }
    }
}