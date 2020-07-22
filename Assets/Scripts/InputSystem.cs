using Leopotam.Ecs;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace RunnerTT
{
    public class InputSystem : IEcsRunSystem
    {
        private EcsWorld _world = null;
        private EcsFilter<InputEvet> _filter;
        public void Run()
        {
            if (_filter.IsEmpty())
                _world.NewEntity().Get<InputEvet>().Value = ButtonPress.None;

            if (Input.GetKeyDown(KeyCode.D))
                SendButtonPressEvent(ButtonPress.Right);
            else if (Input.GetKeyDown(KeyCode.A))
                SendButtonPressEvent(ButtonPress.Left);

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