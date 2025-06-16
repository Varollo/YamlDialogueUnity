using UnityEngine;
using YamlDialogueUnity.Input;

namespace YamlDialogueUnity.View
{
    public abstract class DialogueViewBase : MonoBehaviour
    {
        [SerializeReference] private InputManagerBase inputManager;
        private DialogueController _controller;

        protected virtual void Awake()
        {
            _controller = new(this, inputManager);
            Hide();
        }

        protected virtual void Update()
        {
            _controller.UpdateInput();
        }

        public void Next()
        {
            _controller.Next();
        }

        public void Show(string dialogueStr)
        {
            _controller.InitDialogue(dialogueStr);
            OnShow();
        }

        public void Hide()
        {
            _controller.DropDialogue();
            OnHide();
        }

        public void DisplayOptions(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
                CreateOption(options[i], i);
        }

        public void PickOption(int optionId)
        {
            _controller.PickOption(optionId);
            ClearOptions();
        }

        public virtual void OnHide() { }
        public virtual void OnShow() { }

        public abstract void UpdateView(string actor, string line, string[] actions);
        protected abstract void CreateOption(string option, int id);
        protected abstract void ClearOptions();
    }
}
