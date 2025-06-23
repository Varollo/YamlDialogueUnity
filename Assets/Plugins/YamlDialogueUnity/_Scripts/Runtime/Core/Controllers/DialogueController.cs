using System.Linq;
using YamlDialogueLib;

namespace YamlDialogueUnity
{
    public class DialogueController : IDialogueOptionsListener
    {
        private readonly DialogueOptionsHandler _optionsHandler;
        private readonly DialogueActionsHandler _actionsHandler;
        private readonly ViewSelectionHandler _selectionHandler;

        private readonly DialogueViewBase _dialogueView;
        private readonly DialogueActorViewBase _actorView;
        
        private YamlDialogue _dialogueInstance;

        public DialogueController(DialogueViewBase dialogueView, DialogueActorViewBase actorView)
        {
            _dialogueView = dialogueView;
            _actorView = actorView;

            _optionsHandler = dialogueView.CreateOptionsHandler();
            _actionsHandler = new DialogueActionsHandler();
            _selectionHandler = new ViewSelectionHandler();

            _optionsHandler.AddListener(this);
        }

        public void InitDialogue(string dialogueYaml)
        {
            _dialogueInstance = YamlDialogueParser.Parse(dialogueYaml);
            
            _actorView.Initialize(_actionsHandler);

            OnNextStep();
        }

        public void Next()
        {
            if (_dialogueInstance != null)
                MoveStep(_dialogueInstance.MoveNext());
        }

        private void MoveStep(bool canMoveNext)
        {
            if (canMoveNext)
                OnNextStep();
            else
                _dialogueView.Hide();
        }

        private void OnNextStep()
        {
            var step = _dialogueInstance.Current;

            _dialogueView.UpdateView(step.Actor, step.Line, step.Actions);
            _actorView.SetActor(_dialogueView.GetActorDatabase(), step.Actor, _dialogueView.GetMaxActors());

            SelectableView target = _dialogueView;

            if (step.HasOptions)
            {
                _optionsHandler.CreateOptions(
                    step.Options.Select(o => o.Text).ToArray());

                target = _optionsHandler.GetOptionView(step.ConfirmOption);
            }

            if (step.Actions != null && step.Actions.Length > 0)
                _actionsHandler.BroadcastActions(step.Actions);

            _selectionHandler.SelectView(target);
        }

        public void EndDialogue()
        {            
            _actorView.Clear();
            _optionsHandler.Clear();            

            _selectionHandler.SelectView(null);

            _dialogueInstance = null;
        }

        public void OnPickOption(int optionId)
        {
            bool validMove = _dialogueInstance.MoveToOption(optionId);

            if (validMove)
                _optionsHandler.Clear();

            MoveStep(validMove);
        }

        public void OnCancelOption()
        {
            _selectionHandler.SelectView(_optionsHandler.GetOptionView(
                _dialogueInstance.Current.CancelOption));
        }

        public void OnSelectOptionView(DialogueOptionView view)
        {
            _selectionHandler.SelectView(view);
        }
    }
}
