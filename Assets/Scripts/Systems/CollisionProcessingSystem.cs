using Leopotam.Ecs;
namespace RunnerTT
{
    public class CollisionProcessingSystem : IEcsRunSystem
    {
        private GameState _gameState;
        private SceneData _sceneData;
        private EcsFilter<CollisionEvent> _filter;
        private EcsFilter<PlayerViewRefComponent> _player;
        public void Run()
        {
            foreach(var index in _filter)
            {
                if (_filter.IsEmpty())
                    return;

                CollisionType type = _filter.Get1(index).Type;
                switch(type)
                {
                    case CollisionType.Coin:
                        ref var entity = ref _filter.GetEntity(index);
                        _sceneData.CoinsPool.ReturnToPool(entity.Get<CoinViewRefComponent>().Value);
                        entity.Destroy();
                        _gameState.CoinsCount++;
                        break;

                    case CollisionType.Obstacle:
                        _player.GetEntity(0).Get<PlayerDeathEvent>();
                        break;
                }
            }

        }
    }
}