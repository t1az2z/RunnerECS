using UnityEngine;

namespace RunnerTT
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIScreen : MonoBehaviour
    {
        private Canvas canvas;

        public virtual void OpenScreen()
        {
            CacheCanvas();
            gameObject.SetActive(true);
            canvas.enabled = true;
        }

        public virtual void CloseScreen()
        {
            CacheCanvas();
            canvas.enabled = false;
            gameObject.SetActive(false);
        }

        private void CacheCanvas()
        {
            if (canvas != null)
                return;

            canvas = GetComponent<Canvas>();
        }
    }
}