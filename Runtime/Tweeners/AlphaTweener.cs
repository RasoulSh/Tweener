using UnityEngine;
using UnityEngine.UI;

namespace TweenerSystem.Tweeners
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AlphaTweener : SingleTweener
    {
        [SerializeField] [Range(0f, 1f)] private float from = 0f;
        [SerializeField] [Range(0f, 1f)] private float to = 1f;
        public float From
        {
            get => from;
            set => from = Mathf.Clamp01(value);
        }
        public float To
        {
            get => to;
            set => to = Mathf.Clamp01(value);
        }
        private CanvasGroup _canvasGroup;

        private CanvasGroup CanvasGroup =>
            _canvasGroup != null ? _canvasGroup : _canvasGroup = GetComponent<CanvasGroup>();
        protected override void Animate(float t)
        {
            CanvasGroup.alpha = Mathf.Lerp(from, to, t);
        }
    }
}