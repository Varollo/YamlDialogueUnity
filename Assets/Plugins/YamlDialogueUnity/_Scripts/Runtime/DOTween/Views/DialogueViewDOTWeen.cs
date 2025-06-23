using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace YamlDialogueUnity.DOTween
{
    public class DialogueViewDOTWeen : DialogueView
    {
        [Header("Transition Settings")]
        [SerializeField] DialogueTweener.TweenSettings showTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] DialogueTweener.TweenSettings hideTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] private float lineCharDuration = 0.015f;

        private DialogueTweener _tweener;

        private Tween _actorTxtTween;
        private Tween _lineTxtTween;
        private Tween _panelTween;

        protected override void Awake()
        {
            base.Awake();
            _tweener = new();
        }

        protected override void SetLineTxt(string line, TMP_Text lineTxt)
        {
            if (!string.IsNullOrEmpty(line))
            {
                _lineTxtTween = lineTxt.DOFade(1, (line.Length - 1) * lineCharDuration);

                _lineTxtTween.OnUpdate(() =>
                {
                    lineTxt.text = line[..(int)((line.Length - 1f) * _lineTxtTween.ElapsedPercentage())];
                });

                _lineTxtTween.OnComplete(() =>
                {
                    lineTxt.text = line;
                });
            }
            else lineTxt.text = string.Empty;
        }

        protected override void SetActorTxt(string actor, TMP_Text actorTxt)
        {
            if (!string.IsNullOrEmpty(actor))
            {
                if (actor != actorTxt.text)
                {
                    actorTxt.text = actor;
                    actorTxt.color = new Color(actorTxt.color.r, 
                                               actorTxt.color.g, 
                                               actorTxt.color.b, 
                                               a: 0);
                    _actorTxtTween = actorTxt.DOFade(1f, .1f);
                }
            }
            else actorTxt.text = string.Empty;
        }

        public override IEnumerator OnShow()
        {
            if (_panelTween != null && _panelTween.IsPlaying())
                _panelTween.Complete(true);

            yield return base.OnShow();
            yield return (_panelTween = _tweener.Tween(
                GetCanvasGroup().transform, showTween)).WaitForCompletion();
        }

        public override IEnumerator OnHide()
        {
            if (_panelTween != null && _panelTween.IsPlaying())
                _panelTween.Complete(true);

            yield return (_panelTween = _tweener.Tween(GetCanvasGroup().transform, hideTween))
                .WaitForCompletion();
            yield return base.OnHide();
        }

        public override void OnSubmit()
        {
            bool skipTween = false;

            if (skipTween |= _actorTxtTween != null && _actorTxtTween.active)
                _actorTxtTween.Complete(true);

            if (skipTween |= _lineTxtTween != null && _lineTxtTween.active)
                _lineTxtTween.Complete(true);

            if (!skipTween)
                base.OnSubmit();
        }
    }
}
