using UnityEngine;
using TMPro;

namespace YamlDialogueUnity.View
{
    public class DialogueOptionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text optionTxt;

        private int _optionId;
        private DialogueViewBase _view;

        public void Choose()
        {
            _view.PickOption(_optionId);
        }

        public void CreateInstance(Transform parent, DialogueViewBase view, string text, int id)
        {
            var instance = Instantiate(this, parent);
            
            instance.optionTxt.text = instance.name = text;
            instance._optionId = id;
            instance._view = view;

            // instance.transform.SetAsFirstSibling();
            instance.gameObject.SetActive(true);
        }
    }
}
