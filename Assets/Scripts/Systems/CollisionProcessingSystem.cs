using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class CollisionProcessingSystem : IEcsRunSystem
    {
        private GameState _gameState;
        private SceneData _sceneData;
        private EcsFilter<CollisionEventComponent, CoinViewRefComponent> _filter;
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
                        _gameState.State = State.End;
                        break;
                }
            }
        }
    }
}