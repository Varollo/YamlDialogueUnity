using DG.Tweening;
using UnityEngine;

namespace YamlDialogueUnity.DOTween
{
    public class DialogueActorViewDOTween : DialogueActorView
    {
        [Header("Transition Settings")]
        [SerializeField] private DialogueTweener.TweenSettings showTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] private DialogueTweener.TweenSettings hideTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] private DialogueTweener.TweenSettings focusTween = DialogueTweener.TweenSettings.GetDefault();
        [SerializeField] private DialogueTweener.TweenSettings unfocusTween = DialogueTweener.TweenSettings.GetDefault();

        private DialogueTweener _tweener;
        private Sequence _currentSequence;

        protected override DialogueActorController CreateController(int imgCount, DialogueActionsHandler actionsHandler)
        {
            _tweener = new();
            return base.CreateController(imgCount, actionsHandler);
        }

        public override void OnClearSlot(int slotId) 
        {
            var actorImg = GetActorImg(slotId);

            if (actorImg.isActiveAndEnabled)
                _currentSequence = _tweener.Tween(actorImg.transform, hideTween)
                    .AppendCallback(() => actorImg.enabled = false);
        }

        public override void OnFillSlot(int slotId, Sprite actorSprite)
        {
            var actorImg = GetActorImg(slotId);

            if (actorSprite == null)
            {
                actorImg.enabled = false;
                return;
            }

            actorImg.enabled = true;

            var tween = _tweener.Tween(actorImg.transform, showTween)
                .OnStart(() => actorImg.enabled = (actorImg.sprite = actorSprite) != null);

            if (_currentSequence != null && _currentSequence.IsPlaying())
            {
                _currentSequence = _tweener.Tween(actorImg.transform, hideTween)
                    .Append(tween);
                _currentSequence.Restart();
            }
            else
            {
                _currentSequence = tween;
                _currentSequence.Play();
            }
        }

        public override void OnFocusSlot(int slotId)
        {
            _tweener.Tween(GetActorImg(slotId).transform, focusTween);
        }

        public override void OnUnfocusSlot(int slotId)
        {
            _tweener.Tween(GetActorImg(slotId).transform, unfocusTween);
        }
    }
}
