using Leopotam.Ecs;

namespace RunnerTT
{
    public class ReturnToPoolBehindBorderSystem : IEcsRunSystem
    {
        private SceneData _sceneData = null;
        private Configuration _configuration = null;
        private EcsFilter<WorldObject, Move, ObstacleViewRef> _obstacles = null;
        private EcsFilter<WorldObject, Move, CoinViewRef> _coins = null;

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

        private bool NeedToBePooled(WorldObject worldObjectComponent)
        {
            var position = worldObjectComponent.Transform.position;
            if (position.z <= _configuration.PoolingBorderZCoordinate)
                return true;
            return false;
        }
    }
}