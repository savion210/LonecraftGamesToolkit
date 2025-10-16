#if DOTWEEN
using DG.Tweening;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace LonecraftGames.Toolkit.UI
{
    public class UIButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Animation Settings")]
        [SerializeField] private float hoverScale = 1.1f;
        [SerializeField] private float pressedScale = 0.95f;
        [SerializeField] private float duration = 0.15f;
#if DOTWEEN
        [SerializeField] private Ease easeType = Ease.OutBack;
#endif

        private Vector3 originalScale;
        private Coroutine scaleCoroutine;

#if DOTWEEN
        private Tween currentTween;
#endif

        private void Awake()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            AnimateTo(originalScale * hoverScale);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            AnimateTo(originalScale);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            AnimateTo(originalScale * pressedScale);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            AnimateTo(originalScale * hoverScale);
        }

        private void AnimateTo(Vector3 target)
        {
#if DOTWEEN
            currentTween?.Kill();
            currentTween = transform.DOScale(target, duration).SetEase(easeType);
#else
            if (scaleCoroutine != null)
                StopCoroutine(scaleCoroutine);

            scaleCoroutine = StartCoroutine(ScaleLerp(target, duration));
#endif
        }

#if !DOTWEEN
        private IEnumerator ScaleLerp(Vector3 target, float time)
        {
            Vector3 start = transform.localScale;
            float elapsed = 0f;

            while (elapsed < time)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = elapsed / time;

                // Ease-out-back approximation
                float overshoot = 1.70158f;
                t -= 1f;
                float easedT = (t * t * ((overshoot + 1f) * t + overshoot) + 1f);

                transform.localScale = Vector3.LerpUnclamped(start, target, easedT);
                yield return null;
            }

            transform.localScale = target;
        }
#endif
    }
}
