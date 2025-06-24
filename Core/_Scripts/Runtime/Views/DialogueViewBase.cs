using System;
using System.Collections;
using UnityEngine;

namespace YamlDialogueUnity
{
    public abstract class DialogueViewBase : SelectableView
    {
        private DialogueController _controller;
        private CanvasGroup _mainCanvasGroup;

        public bool IsActive { get; private set; }

        protected CanvasGroup GetCanvasGroup() => _mainCanvasGroup;

        protected override void Awake()
        {
            base.Awake();
            _controller = CreateController();

            _mainCanvasGroup = gameObject.AddComponent<CanvasGroup>();
            _mainCanvasGroup.hideFlags = HideFlags.HideInInspector;
            
            _mainCanvasGroup.alpha = 0;
            _mainCanvasGroup.interactable = false;
            _mainCanvasGroup.blocksRaycasts = false;
        }


        public void Next()
        {
            _controller.Next();
        }

        public void Show(string dialogueStr)
        {
            void showDialogue()
            {
                _mainCanvasGroup.alpha = 1;
                _mainCanvasGroup.interactable = true;
                _mainCanvasGroup.blocksRaycasts = true;

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

        public abstract int GetMaxActors();
        public abstract ActorDatabaseSO GetActorDatabase();
        protected abstract DialogueController CreateController();
        public abstract DialogueOptionsHandler CreateOptionsHandler();
        public abstract void UpdateView(string actor, string line, string[] actions);
    }
}
