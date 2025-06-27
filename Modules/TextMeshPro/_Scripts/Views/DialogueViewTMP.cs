using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YamlDialogueUnity.TextMeshPro
{
    public class DialogueViewTMP : DialogueViewBase, ILegacyUIView
    {
        [Header("TMP References")]
        [SerializeField] private CanvasGroup dialogueBoxPanel;
        [SerializeField] private TMP_Text actorTxt;
        [SerializeField] private TMP_Text lineTxt;

        public CanvasGroup CanvasGroup => dialogueBoxPanel;
        public Graphic ActorName => actorTxt;
        public Graphic Line => lineTxt;

        protected override void OnLineChange(string line)
        {
            lineTxt.text = line;
        }

        protected override void OnActorNameChange(string actor)
        {
            actorTxt.text = actor;
        }

        public override IEnumerator OnShow()
        {
            dialogueBoxPanel.alpha = 1.0f;
            dialogueBoxPanel.blocksRaycasts = true;
            dialogueBoxPanel.interactable = true;

            actorTxt.text = string.Empty;
            lineTxt.text = string.Empty;

            yield break;
        }

        public override IEnumerator OnHide()
        {
            dialogueBoxPanel.alpha = 0f;
            dialogueBoxPanel.blocksRaycasts = false;
            dialogueBoxPanel.interactable = false;

            yield break;
        }
    }
}
