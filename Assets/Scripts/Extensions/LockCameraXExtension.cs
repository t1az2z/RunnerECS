using Cinemachine;
using UnityEngine;

namespace RunnerTT
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LockCameraXExtension : CinemachineExtension
    {
        [Tooltip("Lock the camera's Y position to this value")]
        public float _XPosition = 0;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (enabled && stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = _XPosition;
                state.RawPosition = pos;
            }
        }
    }
}