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
            Sequence sequence = DOTween.Sequence();
            sequence.Append(canvasGroup.DOFade(0, .2f)).Append(canvasGroup.DOFade(1, .2f)).AppendInterval(.8f).SetLoops(-1);
        }
    }
}
