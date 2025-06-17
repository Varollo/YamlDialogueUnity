using System.Linq;
using YamlDialogueLib;

namespace YamlDialogueUnity
{
    public class DialogueController : IDialogueOptionsListener
    {
        private readonly DialogueInputHandler _inputHandler;
        private readonly DialogueOptionsHandler _optionsHandler;
        private readonly DialogueViewBase _dialogueView;
        
        private YamlDialogue _dialogueInstance;

        public DialogueController(DialogueViewBase dialogueView)
        {
            _dialogueView = dialogueView;
            _inputHandler = new DialogueInputHandler();
            _optionsHandler = new DialogueOptionsHandler(dialogueView.OptionPrefab, dialogueView.OptionsHolder);

            _optionsHandler.AddListener(this);
        }

        public bool IsActive { get; internal set; }

        public void InitDialogue(string dialogueYaml)
        {
            _dialogueInstance = YamlDialogueParser.Parse(dialogueYaml);
            OnNextStep();
        }

        public void Next()
        {
            if (_dialogueInstance != null)
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

            _dialogueView.UpdateView(step.Actor, step.Line, step.Actions);

            IDialogueInputTarget target = _dialogueView;

            if (step.HasOptions)
            {
                _optionsHandler.CreateOptions(
                    step.Options.Select(o => o.Text).ToArray());

                target = _optionsHandler.GetOptionView(step.ConfirmOption);
            }     

            SelectInputTarget(target);
        }

        private void EndDialogue()
        {
            _dialogueView.Hide();
            _inputHandler.SelectTarget(null);
        }


        public void DropDialogue()
        {
            SelectInputTarget(null);
            _dialogueInstance = null;
        }

        public void SelectInputTarget(IDialogueInputTarget target)
        {
            _inputHandler.SelectTarget(target);
        }

        public void OnPickOption(int optionId)
        {
            bool validMove = _dialogueInstance.MoveToOption(optionId);

            if (validMove)
                _optionsHandler.ClearOptions();

            MoveStep(validMove);
        }

        public void OnCancelOption()
        {
            SelectInputTarget(_optionsHandler.GetOptionView(
                _dialogueInstance.Current.CancelOption));
        }
    }
}
