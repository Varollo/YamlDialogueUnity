using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace YamlDialogueUnity
{
    public abstract class DialogueViewBase : SelectableView
    {
        [Header("Actor")]
        [SerializeField, Range(0, 2)] private int maxActors;
        [SerializeField] private ActorDatabaseSO actorDatabase;
        [SerializeField] private DialogueActorView actorView;
        [Header("Options")]
        [SerializeReference] private DialogueOptionViewBase optionPrefab;
        [SerializeField] private Transform optionHolder;        
        [Header("Events")]
        public DialogueStepEvent OnStep;
        public DialogueActionsEvent OnActions;

        private DialogueController _controller;

        public bool IsActive { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            var optionsHandler = new DialogueOptionsHandler(optionPrefab, optionHolder);
            _controller = new DialogueController(this, actorView, optionsHandler);
            
            gameObject.SetActive(false);
        }

        public void UpdateView(string actor, string line, string[] actions)
        {
            OnActorNameChange(actor);
            OnLineChange(line);

            OnStep?.Invoke(actor, line, actions);

            if (actions != null && actions.Length > 0)
                OnActions?.Invoke(actions);
        }

        public void Next()
        {
            _controller.Next();
        }

        public void Show(string dialogueStr)
        {
            void showDialogue()
            {
                gameObject.SetActive(true);

                StartCoroutine(CallbackCo(OnShow(), () =>
                {
                    _controller.InitDialogue(dialogueStr);
                }));
            }

            if (!IsActive)
                showDialogue();
            else
                Hide(afterHide: showDialogue);

            IsActive = true;
        }

        public void Hide() => Hide(null);
        private void Hide(Action afterHide)
        {
            _controller.EndDialogue();

            StartCoroutine(CallbackCo(OnHide(), () =>
            {
                afterHide?.Invoke();
            }));

            IsActive = false;
        }

        private IEnumerator CallbackCo(IEnumerator coroutine, Action callback)
        {
            yield return StartCoroutine(coroutine);
            callback?.Invoke();
        }

        public override void OnSubmit()
        {
            Next();
        }

        public virtual IEnumerator OnHide() 
        { 
            yield break; 
        }

        public virtual IEnumerator OnShow()
        {
            yield break;
        }

        public int GetMaxActors()
        {
            return maxActors;
        }

        public ActorDatabaseSO GetActorDatabase()
        {
            return actorDatabase;
        }

        protected abstract void OnLineChange(string line);
        protected abstract void OnActorNameChange(string actor);
        
        [Serializable] public class DialogueStepEvent : UnityEvent<string, string, string[]> { }
        [Serializable] public class DialogueActionsEvent : UnityEvent<string[]> { }
    }
}
