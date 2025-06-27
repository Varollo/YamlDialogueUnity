using UnityEngine;
using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public class DialogueOptionView : DialogueOptionViewBase
    {
        [SerializeField] private Text optionTxt;

        protected override void SetOptionTxt(string text)
        {
            optionTxt.text = text;
        }
    }
}
