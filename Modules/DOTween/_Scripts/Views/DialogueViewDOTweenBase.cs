using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace YamlDialogueUnity.DOTween
{
    public abstract class DialogueViewDOTweenBase<TText> : DialogueViewOldUI<TText>, ITweenableText where TText : Graphic
    {
        [Header("Transition Settings")]
        [SerializeField] DialogueTweener.TweenSettings showTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] DialogueTweener.TweenSettings hideTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] private float lineCharDuration = 0.015f;

        private DialogueTweener _tweener;
        private DialogueTextTweener _textTweener;

        private Tween _panelTween;

        protected override void Awake()
        {
            base.Awake();
            _tweener = new();
            _textTweener = new(this);
        }

        protected override void SetLine(string line, TText lineTxt)
        {
            _textTweener.TweenLine(line, lineCharDuration, lineTxt);
        }

        protected override void SetActorName(string actorName, TText actorTxt)
        {
            _textTweener.TweenActorName(actorName, .1f, actorTxt);
        }

        public override IEnumerator OnShow()
        {
            if (_panelTween != null && _panelTween.IsPlaying())
                _panelTween.Complete(true);

            yield return base.OnShow();
            yield return (_panelTween = _tweener.Tween(
                CanvasGroup.transform, showTween)).WaitForCompletion();
        }

        public override IEnumerator OnHide()
        {
            if (_panelTween != null && _panelTween.IsPlaying())
                _panelTween.Complete(true);

            yield return (_panelTween = _tweener.Tween(CanvasGroup.transform, hideTween))
                .WaitForCompletion();
            yield return base.OnHide();
        }

        public override void OnSubmit()
        {            
            if (!_textTweener.SkipLine())
                base.OnSubmit();
        }

        public abstract void UpdateLineTxt(string line);
        public abstract void UpdateActorTxt(string actorName);
    }
}
