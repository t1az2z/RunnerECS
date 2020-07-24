using DG.Tweening;
using UnityEngine;

namespace RunnerTT
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TextBlink : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private void OnEnable()
        {
            if (!canvasGroup)
                canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(0, .5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
