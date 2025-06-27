using TMPro;
using UnityEngine;

namespace YamlDialogueUnity.TextMeshPro
{
    public class DialogueOptionViewTMP : DialogueOptionViewBase
    {
        [SerializeField] private TMP_Text optionTxt;

        protected override void SetOptionTxt(string text)
        {
            optionTxt.text = text;
        }
    }
}
