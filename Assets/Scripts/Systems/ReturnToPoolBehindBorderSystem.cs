using Leopotam.Ecs;

namespace RunnerTT
{
    public class ReturnToPoolBehindBorderSystem : IEcsRunSystem
    {
        private SceneData _sceneData = null;
        private Configuration _configuration = null;
        private EcsFilter<WorldObjectComponent, MoveComponent, ObstacleViewRefComponent> _obstacles = null;
        private EcsFilter<WorldObjectComponent, MoveComponent, CoinViewRefComponent> _coins = null;

        public void Run()
        {
            foreach (var index in _obstacles)
            {
                var worldObjectComponent = _obstacles.Get1(index);
                if (NeedToBePooled(worldObjectComponent))
                {
                    ref var obstacleViewRef = ref _obstacles.Get3(index);
                    _sceneData.ObsaclesPool.ReturnToPool(obstacleViewRef.Value);
                }
            }

            foreach (var index in _coins)
            {
                var worldObjectComponent = _coins.Get1(index);
                if (NeedToBePooled(worldObjectComponent))
                {
                    ref var coinViewRef = ref _coins.Get3(index);
                    _sceneData.CoinsPool.ReturnToPool(coinViewRef.Value);
                }
            }
        }

        private bool NeedToBePooled(WorldObjectComponent worldObjectComponent)
        {
            var position = worldObjectComponent.Transform.position;
            if (position.z <= _configuration.PoolingBorderZCoordinate)
                return true;
            return false;
        }
    }
}