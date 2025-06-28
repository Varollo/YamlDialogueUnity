using UnityEngine;
using UnityEngine.UIElements;

namespace YamlDialogueUnity.UIToolkit.Example
{
    public class ExampleManagerUIToolkit : MonoBehaviour
    {
        [SerializeField] private TextAsset dialogueFile1, dialogueFile2;
        [SerializeField] private UIDocument uiDoc;

        private VisualElement _uiRoot;
        private DialogueViewUIToolkit _dialogueView;

        private void Start()
        {
            _uiRoot = uiDoc.rootVisualElement;

            _dialogueView = _uiRoot.Q<DialogueViewUIToolkit>();

            _uiRoot.Q<Button>("Ex01Btn").clicked += ShowDiaogue1;
            _uiRoot.Q<Button>("Ex02Btn").clicked += ShowDiaogue2;
        }

        [ContextMenu("Play Dialogue 1")]
        public void ShowDiaogue1()
        {
            _dialogueView.Show(dialogueFile1);
        }

        [ContextMenu("Play Dialogue 2")]
        public void ShowDiaogue2()
        {
            _dialogueView.Show(dialogueFile2);
        }

        public void OnDialogueStep(string actor, string line, string[] actions)
        {
            _uiRoot.Q<Label>(className: ".info").text = actions != null && actions.Length > 0
                ? string.Join("\n", actions)
                : string.Empty;
        }
    }
}
