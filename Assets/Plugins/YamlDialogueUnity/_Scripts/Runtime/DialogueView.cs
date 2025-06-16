using UnityEngine;
using TMPro;

namespace YamlDialogueUnity
{
    [RequireComponent(typeof(DialogueInputManager))]
    public class DialogueView : MonoBehaviour
    {
        private DialogueController _controller;

        [SerializeField] private bool hideOnCompletion = true;        
        [Header("References")]
        [SerializeField] private TMP_Text actorTxt;
        [SerializeField] private TMP_Text lineTxt;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private DialogueOptionView optionPrefab;
        [SerializeField] private Transform optionsHolder;

        private void Awake()
        {
            var input = GetComponent<DialogueInputManager>();
            _controller = new(input, this);

            Hide();
        }

        public void Next()
        {
            _controller.Next();
        }

        public void UpdateView(string actor, string line, string[] actions)
        {
            actorTxt.text = actor;
            lineTxt.text = line;
            
            ClearOptions();
        }

        private void ClearOptions()
        {
            for (int i = 0; i < optionsHolder.childCount; i++)
            {                
                if (optionsHolder.GetChild(i).gameObject != optionPrefab.gameObject)
                    Destroy(optionsHolder.GetChild(i).gameObject, Time.deltaTime);
            } 
        }

        public void Show(TextAsset dialogueFile)
        {
            group.alpha = 1.0f;
            group.blocksRaycasts = group.interactable = true;
            
            _controller.InitDialogue(dialogueFile.text);
        }

        public void Hide()
        {
            group.alpha = 0f;
            group.blocksRaycasts = group.interactable = true;
        }

        public void DisplayOptions(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                optionPrefab.CreateInstance(
                    optionsHolder, _controller, options[i], i);
            }
        }
    }
}
