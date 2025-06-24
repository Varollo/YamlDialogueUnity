using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace YamlDialogueUnity
{
    public class DialogueView : DialogueViewBase
    {
        [System.Serializable] public class DialogueStepEvent : UnityEvent<string, string, string[]> { }
        [System.Serializable] public class DialogueActionsEvent : UnityEvent<string[]> { }

        [Header("Actor")]
        [SerializeField, Range(0,2)] private int maxActors;
        [SerializeField] private ActorDatabaseSO actorDatabase;
        [SerializeField] private DialogueActorView actorView;
        [Header("Options")]
        [SerializeField] private DialogueOptionView optionPrefab;
        [SerializeField] private Transform optionHolder;
        [Header("Dialogue Box")]
        [SerializeField] private CanvasGroup dialogueBoxPanel;
        [SerializeField] private TMP_Text actorTxt;
        [SerializeField] private TMP_Text lineTxt;
        [Header("Events")]
        public DialogueStepEvent OnStep;
        public DialogueActionsEvent OnActions;

        public override int GetMaxActors() => maxActors;
        public override ActorDatabaseSO GetActorDatabase() => actorDatabase;
        
        protected override DialogueController CreateController() => new(this, actorView);
        public override DialogueOptionsHandler CreateOptionsHandler() => new(optionPrefab, optionHolder);

        public override void UpdateView(string actor, string line, string[] actions)
        {
            SetActorTxt(actor, actorTxt);
            SetLineTxt(line, lineTxt);

            OnStep?.Invoke(actor, line, actions);

            if (actions != null && actions.Length > 0)
                OnActions?.Invoke(actions);
        }

        protected virtual void SetLineTxt(string line, TMP_Text lineTxt)
        {
            lineTxt.text = line;
        }

        protected virtual void SetActorTxt(string actor, TMP_Text actorTxt)
        {
            actorTxt.text = actor;
        }

        protected virtual void SetActorImage(string actor, ActorDatabaseSO actorDatabase, Image actorImg, bool actorChanged)
        {
            if (!actorChanged)
                return;

            var actorSprite = actorDatabase.GetActorSprite(actor);
            
            if(actorImg.enabled = actorSprite != null)
                actorImg.sprite = actorSprite;
        }

        public override IEnumerator OnShow()
        {
            dialogueBoxPanel.alpha = 1.0f;
            dialogueBoxPanel.blocksRaycasts = dialogueBoxPanel.interactable = true;
            actorTxt.text = string.Empty;
            lineTxt.text = string.Empty;
            yield break;
        }

        public override IEnumerator OnHide()
        {
            dialogueBoxPanel.alpha = 0f;
            dialogueBoxPanel.blocksRaycasts = dialogueBoxPanel.interactable = false;
            yield break;
        }
    }
}
