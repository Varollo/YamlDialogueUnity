using System.Linq;
using YamlDialogueLib;
using YamlDialogueUnity.Input;
using YamlDialogueUnity.View;

namespace YamlDialogueUnity
{
    public class DialogueController
    {
        private readonly InputManagerBase _inputManager;
        private readonly DialogueViewBase _view;
        
        private YamlDialogue _dialogueInstance;

        public DialogueController(DialogueViewBase view, InputManagerBase inputManager)
        {
            _inputManager = inputManager;
            _view = view;
        }

        public bool IsActive { get; internal set; }

        public void InitDialogue(string dialogueYaml)
        {
            _dialogueInstance = YamlDialogueParser.Parse(dialogueYaml);
            OnNextStep();
        }

        public void Next()
        {
            MoveStep(_dialogueInstance.MoveNext());
        }

        private void MoveStep(bool canMove)
        {
            if (canMove)
                OnNextStep();
            else
                EndDialogue();
        }

        private void OnNextStep()
        {
            var step = _dialogueInstance.Current;

            _view.UpdateView(step.Actor, step.Line, step.Actions);

            if (step.HasOptions)
                HandleOptions(step.Options);
            else
                _inputManager.Enable();
        }

        private void HandleOptions(YamlDialogueOption[] options)
        {
            _inputManager.Disable();
            _view.DisplayOptions(options.Select(o => o.Text).ToArray());
        }

        private void EndDialogue()
        {
            _view.Hide();
            _inputManager.Disable();
        }

        public void PickOption(int optionId)
        {
            MoveStep(_dialogueInstance.MoveToOption(optionId));
        }

        public void DropDialogue()
        {
            _dialogueInstance = null;
        }

        internal void UpdateInput()
        {
            if (_inputManager.CheckInput())
                Next();
        }
    }
}
