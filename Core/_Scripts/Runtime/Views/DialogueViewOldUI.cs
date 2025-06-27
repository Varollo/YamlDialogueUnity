using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public abstract class DialogueViewOldUI<TText> : DialogueViewBase where TText : Graphic
    {
        [Header("UI References")]
        [SerializeField] private CanvasGroup dialogueBoxPanel;
        [SerializeReference] private TText actorTxt;
        [SerializeReference] private TText lineTxt;

        protected CanvasGroup CanvasGroup => dialogueBoxPanel;
        protected TText Actor => actorTxt;
        protected TText Line => lineTxt;

        public override IEnumerator OnShow()
        {
            dialogueBoxPanel.alpha = 1.0f;
            dialogueBoxPanel.blocksRaycasts = dialogueBoxPanel.interactable = true;
            
            OnLineChange(string.Empty);            
            OnActorNameChange(string.Empty);

            yield break;
        }

        public override IEnumerator OnHide()
        {
            dialogueBoxPanel.alpha = 0f;
            dialogueBoxPanel.blocksRaycasts = dialogueBoxPanel.interactable = false;

            yield break;
        }

        protected override void OnLineChange(string line)
        {
            SetLine(line, Line);
        }

        protected override void OnActorNameChange(string actor)
        {
            SetActorName(actor, Actor);
        }

        protected abstract void SetLine(string line, TText lineTxt);
        protected abstract void SetActorName(string actorName, TText actorTxt);
    }
}
