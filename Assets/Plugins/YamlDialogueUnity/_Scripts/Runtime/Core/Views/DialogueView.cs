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
        
        protected TMP_Text ActorTxt => actorTxt;
        protected TMP_Text LineTxt => lineTxt;
        protected CanvasGroup Group => group;

        public override void UpdateView(string actor, string line, string[] actions)
        {
            ActorTxt.text = actor;
            LineTxt.text = line;
        }

        public override void OnShow()
        {
            Group.alpha = 1.0f;
            Group.blocksRaycasts = Group.interactable = true;            
        }

        public override void OnHide()
        {
            Group.alpha = 0f;
            Group.blocksRaycasts = Group.interactable = false;
        }
    }
}
