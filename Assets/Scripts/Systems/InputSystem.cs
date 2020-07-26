using Leopotam.Ecs;
using UnityEngine;

namespace RunnerTT
{
    public class InputSystem : IEcsRunSystem
    {
        private GameState _gameState = null;
        private EcsWorld _world = null;
        private EcsFilter<InputEvet> _filter = null;

        public void Run()
        {
            if (_filter.IsEmpty())
                _world.NewEntity().Get<InputEvet>().Value = ButtonPress.None;

            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                if (_gameState.State == State.Game)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                        SendButtonPressEvent(ButtonPress.Right, entity);
                    else if (Input.GetKeyDown(KeyCode.A))
                        SendButtonPressEvent(ButtonPress.Left, entity);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                    SendButtonPressEvent(ButtonPress.Space, entity);
            }
        }
        public void SendButtonPressEvent(ButtonPress buttonPress, EcsEntity entity)
        {
            entity.Get<InputEvet>().Value = buttonPress;
        }
    }
    public enum ButtonPress
    {
        Left,
        Right,
        Space,
        None
    }
}