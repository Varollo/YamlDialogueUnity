using System;
using System.Linq;
using YamlDialogueLib;

namespace YamlDialogueUnity
{
    public class DialogueController
    {
        private YamlDialogue _dialogueInstance;
        private DialogueInputManager _input;
        private DialogueView _view;

        public DialogueController(DialogueInputManager input, DialogueView view)
        {
            _input = input;
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
                _input.Enable();
        }

        private void HandleOptions(YamlDialogueOption[] options)
        {
            _input.Disable();
            _view.DisplayOptions(options.Select(o => o.Text).ToArray());
        }

        private void EndDialogue()
        {
            _view.Hide();
            _input.Disable();
        }

        public void PickOption(int optionId)
        {
            MoveStep(_dialogueInstance.MoveToOption(optionId));
        }
    }
}
