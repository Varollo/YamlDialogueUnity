using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace YamlDialogueUnity
{
    public class DialogueOptionView : MonoBehaviour, IDialogueInputTarget
    {
        [SerializeField] private TMP_Text optionTxt;
        [SerializeField] private Button optionBtn;

        private int _optionId;
        private DialogueOptionsHandler _handler;

        public DialogueOptionView CreateInstance(Transform parent, DialogueOptionsHandler handler, string text, int id)
        {
            var instance = Instantiate(this, parent);
            
            instance.optionTxt.text = instance.name = text;
            instance._optionId = id;
            instance._handler = handler;

            instance.gameObject.SetActive(true);
            return instance;
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public void OnCancel(BaseEventData eventData)
        {
            _handler.SelectCancelOption();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _handler.PickOption(_optionId);
        }

        public virtual void OnDeselect() { }
        public virtual void OnSelect() { }
    }
}
