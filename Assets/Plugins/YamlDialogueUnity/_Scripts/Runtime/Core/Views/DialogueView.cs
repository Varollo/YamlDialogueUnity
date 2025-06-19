using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public class DialogueView : DialogueViewBase
    {
        [Header("Configuration")]
        [SerializeField] private ActorDatabaseSO actorDatabase;
        [Header("Options")]
        [SerializeField] private DialogueOptionView optionPrefab;
        [SerializeField] private Transform optionHolder;
        [Header("References")]
        [SerializeField] private Image actorImg;
        [SerializeField] private TMP_Text actorTxt;
        [SerializeField] private TMP_Text lineTxt;
        [SerializeField] private CanvasGroup group;

        public override DialogueOptionView OptionPrefab => optionPrefab;
        public override Transform OptionsHolder => optionHolder;
        
        protected CanvasGroup Group => group;

        public override void UpdateView(string actor, string line, string[] actions)
        {
            SetActorImage(actor, actorDatabase, actorImg, actor != actorTxt.text);
            SetActorTxt(actor, actorTxt);
            SetLineTxt(line, lineTxt);
        }

        protected virtual void SetLineTxt(string line, TMP_Text lineTxt)
        {
            lineTxt.text = line;
        }

        protected virtual void SetActorTxt(string actor, TMP_Text actorTxt)
        {
            actorTxt.text = actor;
        }

        protected virtual void SetActorImage(string actor, ActorDatabaseSO actorDatabase, Image actorImg, bool actorChanged)
        {
            if (!actorChanged)
                return;

            var actorSprite = actorDatabase.GetActorSprite(actor);
            
            if(actorImg.enabled = actorSprite != null)
                actorImg.sprite = actorSprite;
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
