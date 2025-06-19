using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YamlDialogueUnity.DOTween
{
    public class DialogueViewDOTWeen : DialogueView
    {
        [Header("Transition Settings")]
        [SerializeField] DialogueTweener.TweenSettings showTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] DialogueTweener.TweenSettings hideTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] DialogueTweener.TweenSettings showActorImgTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] DialogueTweener.TweenSettings hideActorImgTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] private float lineCharDuration = 0.015f;

        private DialogueTweener _tweener;

        private Tween ActorTxtTween;
        private Tween LineTxtTween;

        protected override void Awake()
        {
            base.Awake();
            _tweener = new();
        }

        protected override void SetLineTxt(string line, TMP_Text lineTxt)
        {
            if (!string.IsNullOrEmpty(line))
            {
                LineTxtTween = lineTxt.DOFade(1, (line.Length - 1) * lineCharDuration);

                LineTxtTween.OnUpdate(() =>
                {
                    lineTxt.text = line[..(int)((line.Length - 1f) * LineTxtTween.ElapsedPercentage())];
                });

                LineTxtTween.OnComplete(() =>
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
                    ActorTxtTween = actorTxt.DOFade(1f, .1f);
                }
            }
            else actorTxt.text = string.Empty;
        }

        protected override void SetActorImage(string actor, ActorDatabaseSO actorDatabase, Image actorImg, bool actorChanged)
        {            
            if (actorChanged)
            {
                var showTween = _tweener.Tween(actorImg.transform, showActorImgTween).OnStart(() =>
                {
                    actorImg.sprite = actorDatabase.GetActorSprite(actor);
                    actorImg.enabled = actorImg.sprite != null;
                });
                _tweener.Tween(actorImg.transform, hideActorImgTween).Append(showTween);
            }
        }

        public override void OnShow()
        {
            _tweener.Tween(Group.transform, showTween);
        }

        public override void OnHide()
        {
            _tweener.Tween(Group.transform, hideTween);
        }

        public override void OnSubmit()
        {
            bool skipTween = false;

            if (skipTween |= ActorTxtTween != null && ActorTxtTween.active)
                ActorTxtTween.Complete(true);

            if (skipTween |= LineTxtTween != null && LineTxtTween.active)
                LineTxtTween.Complete(true);

            if (!skipTween)
                base.OnSubmit();
        }
    }
}
