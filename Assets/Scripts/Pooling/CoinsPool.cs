using Leopotam.Ecs;

namespace RunnerTT
{
    public class CoinsPool : GenericObjectPool<CoinView>
    {
        public override void ReturnToPool(CoinView objectToReturn)
        {
            var entity = objectToReturn.Entity;
            entity.Del<WorldObjectComponent>();
            entity.Del<CoinViewRefComponent>();
            entity.Del<MoveComponent>();
            base.ReturnToPool(objectToReturn);
        }
    }
}
