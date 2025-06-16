using UnityEngine;
using TMPro;

namespace YamlDialogueUnity.View
{
    public class DialogueView : DialogueViewBase
    {    
        [Header("References")]
        [SerializeField] private TMP_Text actorTxt;
        [SerializeField] private TMP_Text lineTxt;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private DialogueOptionView optionPrefab;
        [SerializeField] private Transform optionsHolder;

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

        protected override void ClearOptions()
        {
            for (int i = 0; i < optionsHolder.childCount; i++)
                if (optionsHolder.GetChild(i).gameObject != optionPrefab.gameObject)
                    Destroy(optionsHolder.GetChild(i).gameObject, Time.deltaTime);
        }

        protected override void CreateOption(string optionTxt, int id)
        {
            optionPrefab.CreateInstance(optionsHolder, this, optionTxt, id);
        }
    }
}
