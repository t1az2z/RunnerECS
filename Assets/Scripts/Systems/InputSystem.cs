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
            if (_gameState.State == State.Game)
            {
                if (_filter.IsEmpty())
                    _world.NewEntity().Get<InputEvet>().Value = ButtonPress.None;

                if (Input.GetKeyDown(KeyCode.D))
                    SendButtonPressEvent(ButtonPress.Right);
                else if (Input.GetKeyDown(KeyCode.A))
                    SendButtonPressEvent(ButtonPress.Left);

            }

            if (Input.GetKeyDown(KeyCode.Space))
                SendButtonPressEvent(ButtonPress.Space);
        }
        public void SendButtonPressEvent(ButtonPress buttonPress)
        {
            _world.NewEntity().Get<InputEvet>().Value = buttonPress;
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