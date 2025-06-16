using UnityEngine;
using TMPro;

namespace YamlDialogueUnity
{
    public class DialogueOptionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text optionTxt;

        private int _optionId;
        private DialogueController _controller;

        public void Choose()
        {
            _controller.PickOption(_optionId);
        }

        public void CreateInstance(Transform parent, DialogueController controller, string text, int id)
        {
            var instance = Instantiate(this, parent);
            
            instance.optionTxt.text = instance.name = text;
            instance._optionId = id;
            instance._controller = controller;

            // instance.transform.SetAsFirstSibling();
            instance.gameObject.SetActive(true);
        }
    }
}
