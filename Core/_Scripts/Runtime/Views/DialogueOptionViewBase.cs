using UnityEngine;
using UnityEngine.EventSystems;

namespace YamlDialogueUnity
{
    public abstract class DialogueOptionViewBase : SelectableView, ISelectHandler
    {
        private DialogueOptionsHandler _handler;

        private int _optionId;

        public DialogueOptionViewBase CreateInstance(Transform parent, DialogueOptionsHandler handler, string text, int id)
        {
            var instance = Instantiate(this, parent);

            instance.name = text;
            instance._optionId = id;
            instance._handler = handler;
            instance.SetOptionTxt(text);

            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public override void OnCancel()
        {
            _handler.SelectCancelOption();
        }

        public override void OnSubmit()
        {
            _handler.PickOption(_optionId);
        }

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            _handler.SelectOptionView(this);
        }

        protected abstract void SetOptionTxt(string text);
    }
}
