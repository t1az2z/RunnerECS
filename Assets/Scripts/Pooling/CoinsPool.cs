using Leopotam.Ecs;

namespace RunnerTT
{
    public class CoinsPool : GenericObjectPool<CoinView>
    {
        public override void ReturnToPool(CoinView objectToReturn)
        {
            var entity = objectToReturn.Entity;
            entity.Del<WorldObject>();
            entity.Del<CoinViewRef>();
            entity.Del<Move>();
            base.ReturnToPool(objectToReturn);
        }
    }
}
