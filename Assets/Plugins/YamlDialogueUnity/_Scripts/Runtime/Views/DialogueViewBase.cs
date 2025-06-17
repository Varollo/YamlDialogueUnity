using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public abstract class DialogueViewBase : MonoBehaviour, IDialogueInputTarget
    {
        private DialogueController _controller;

        public abstract DialogueOptionView OptionPrefab { get; }
        public abstract Transform OptionsHolder { get; }

        protected virtual void Awake()
        {
            _controller = new DialogueController(this);
        }

        protected virtual void OnEnable()
        {            
            if (EventSystem.current != null)
                EventSystem.current.SetSelectedGameObject(gameObject);
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

        private void AdvanceIfSelected(GameObject selected)
        {
            if (selected == gameObject)
                Next();
        }

        public virtual void OnSubmit(BaseEventData eventData) => AdvanceIfSelected(eventData.selectedObject);
        public virtual void OnCancel(BaseEventData eventData) => AdvanceIfSelected(eventData.selectedObject);

        public virtual void OnSelect() { }
        public virtual void OnDeselect() { }

        public virtual void OnHide() { }
        public virtual void OnShow() { }

        public abstract void UpdateView(string actor, string line, string[] actions);
    }
}
