using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace YamlDialogueUnity.DOTween
{
    public class DialogueViewDOTWeen : DialogueView
    {
        [Header("Transition Settings")]
        [SerializeField] private float showDuration = 0.25f;
        [SerializeField] private float charDuration = 0.015f;
        [SerializeField] private Ease ease = Ease.InOutQuad;

        private Tween ActorTxtTween;
        private Tween LineTxtTween;

        public override void UpdateView(string actor, string line, string[] actions)
        {
            if (!string.IsNullOrEmpty(actor))
            {
                if (actor != ActorTxt.text)
                {
                    ActorTxt.text = actor;
                    ActorTxt.color = ActorTxt.color * new Color(1, 1, 1, 0);
                    ActorTxtTween = ActorTxt.DOFade(1f,  .1f);
                }
            }
            else ActorTxt.text = string.Empty;

            if (!string.IsNullOrEmpty(line))
            {
                LineTxtTween = LineTxt.DOFade(1, (line.Length - 1) * charDuration);

                LineTxtTween.OnUpdate(() =>
                {
                    LineTxt.text = line[..(int)((line.Length - 1f) * LineTxtTween.ElapsedPercentage())];
                });

                LineTxtTween.OnComplete(() =>
                {
                    LineTxt.text = line;
                });
            }
            else LineTxt.text = string.Empty;
        }

        public override void OnShow()
        {
            var panel = Group.transform as RectTransform;
            var canvas = panel.GetComponentInParent<CanvasScaler>();

            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, -canvas.referenceResolution.y * canvas.scaleFactor);
            panel.DOAnchorPosY(0, showDuration).SetEase(ease);
        }

        public override void OnHide()
        {
            var panel = Group.transform as RectTransform;
            var canvas = panel.GetComponentInParent<CanvasScaler>();

            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, 0);
            panel.DOAnchorPosY(-canvas.referenceResolution.y * canvas.scaleFactor, showDuration).SetEase(ease);
        }

        public override void OnSubmit()
        {
            bool skipTween = false;

            if (skipTween |= ActorTxtTween != null && ActorTxtTween.active)
                ActorTxtTween.Complete(true);

            if (skipTween |= (LineTxtTween != null && LineTxtTween.active))
                LineTxtTween.Complete(true);

            if (!skipTween)
                base.OnSubmit();
        }
    }
}
