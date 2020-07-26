using Leopotam.Ecs;
namespace RunnerTT
{
    public class CollisionProcessingSystem : IEcsRunSystem
    {
        private GameState _gameState = null;
        private SceneData _sceneData = null;
        private EcsFilter<CollisionEvent> _filter = null;
        private EcsFilter<PlayerViewRef> _player = null;

        public void Run()
        {
            foreach(var index in _filter)
            {
                CollisionType type = _filter.Get1(index).Type;
                switch(type)
                {
                    case CollisionType.Coin:
                        ref var entity = ref _filter.GetEntity(index);
                        _sceneData.CoinsPool.ReturnToPool(entity.Get<CoinViewRef>().Value);
                        entity.Destroy();
                        _gameState.CoinsCount++;
                        break;

                    case CollisionType.Obstacle:
                        _player.GetEntity(index).Get<PlayerDeathEvent>();
                        break;
                }
            }

        }
    }
}