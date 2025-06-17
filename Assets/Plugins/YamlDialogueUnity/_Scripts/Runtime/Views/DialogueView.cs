using UnityEngine;
using TMPro;

namespace YamlDialogueUnity
{
    public class DialogueView : DialogueViewBase
    {    
        [Header("References")]
        [SerializeField] private TMP_Text actorTxt;
        [SerializeField] private TMP_Text lineTxt;
        [SerializeField] private CanvasGroup group;
        [Header("Options")]
        [SerializeField] private DialogueOptionView optionPrefab;
        [SerializeField] private Transform optionHolder;

        public override DialogueOptionView OptionPrefab => optionPrefab;
        public override Transform OptionsHolder => optionHolder;

        public override void UpdateView(string actor, string line, string[] actions)
        {
            actorTxt.text = actor;
            lineTxt.text = line;
        }

        public override void OnShow()
        {
            group.alpha = 1.0f;
            group.blocksRaycasts = group.interactable = true;            
        }

        public override void OnHide()
        {
            group.alpha = 0f;
            group.blocksRaycasts = group.interactable = false;
        }
    }
}
