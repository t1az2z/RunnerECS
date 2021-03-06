using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;
        public Configuration Configuration;
        public SceneData SceneData;

        void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            GameState gameState = new GameState(Configuration.MovementSpeed);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                .Add(new PlayerInitializationSystem())
                .Add(new InputSystem())
                .Add(new InputProcessingSystem())

                .Add(new PlayerMovementSystem())
                .Add(new WorldMovementSystem())

                .Add(new SpawnPointsInitializationSystem())
                .Add(new GenerationPlanningSystem())
                .Add(new ObstaclesSpawnSystem())
                .Add(new CoinsSpawnSystem())
                .Add(new ReturnToPoolBehindBorderSystem())

                .Add(new CollisionProcessingSystem())
                .Add(new PlayerDeathProcessingSystem())

                .Add(new DistanceCounterInitSystem())
                .Add(new DistanceCountSystem())
                
                .OneFrame<InputEvet> ()
                .OneFrame<SpawnObstacleEvent>()
                .OneFrame<SpawnCoinEvent>()
                .OneFrame<CollisionEvent>()
                .OneFrame<PlayerDeathEvent>()
                
                .Inject(Configuration)
                .Inject(SceneData)
                .Inject(gameState)
                .Init ();

            SceneData.CoinsPool.Prewarm(100, Configuration.CoinPrefab);
            SceneData.ObsaclesPool.Prewarm(100, Configuration.ObstaclePrefab);
            GameState.OnGameStateChange += SceneData.UI.OnGameStateChange;

            gameState.State = State.Start;
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
            GameState.OnGameStateChange -= SceneData.UI.OnGameStateChange;
        }
    }
}