using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;
        public Configuration Configuration;
        public SceneData SceneData;

        void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            GameState gameState = new GameState(Configuration.MovementSpeed);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your systems here, for example:
                .Add(new PlayerInitializationSystem())
                .Add(new DistanceCounterInitSystem())
                .Add(new DistanceCountSystem())
                .Add(new InputSystem())
                .Add(new WorldMovementSystem())
                .Add(new InputProcessingSystem())
                .Add(new PlayerMovementSystem())
                .Add(new SpawnPointsInitializationSystem())
                .Add(new GenerationPlanningSystem())
                .Add(new SpawnObstaclesSystem())
                .Add(new SpawnCoinsSystem())
                .Add(new CollisionProcessingSystem())
                .Add(new ReturnToPoolBehindBorderSystem())
                // .Add (new TestSystem2 ())
                
                // register one-frame components (order is important), for example:
                .OneFrame<InputEvet> ()
                .OneFrame<SpawnObstacleEvent>()
                .OneFrame<SpawnCoinEvent>()
                .OneFrame<CollisionEventComponent>()
                // .OneFrame<TestComponent2> ()
                
                // inject service instances here (order doesn't important), for example:
                .Inject(Configuration)
                .Inject(SceneData)
                .Inject(gameState)
                // .Inject (new NavMeshSupport ())
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
        }
    }
}