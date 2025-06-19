using UnityEngine;

namespace YamlDialogueUnity
{
    public abstract class DialogueViewBase : SelectableView
    {
        private DialogueController _controller;

        public abstract DialogueOptionView OptionPrefab { get; }
        public abstract Transform OptionsHolder { get; }

        protected override void Awake()
        {
            base.Awake();
            _controller = new DialogueController(this);
        }

        public void Next()
        {
            _controller.Next();
        }

        public void Show(string dialogueStr)
        {
            if (!isActiveAndEnabled)
            {
                gameObject.SetActive(true);
                enabled = true;
            }

            _controller.InitDialogue(dialogueStr);
            OnShow();
        }

        public void Hide()
        {
            _controller.DropDialogue();
            OnHide();
        }

        public override void OnSubmit()
        {
            Next();
        }

        public virtual void OnHide() { }
        public virtual void OnShow() { }

        public abstract void UpdateView(string actor, string line, string[] actions);
    }
}
