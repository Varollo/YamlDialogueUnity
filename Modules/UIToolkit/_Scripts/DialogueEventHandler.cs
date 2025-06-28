using YamlDialogueLib;

namespace YamlDialogueUnity.UIToolkit
{
    public partial class DialogueControllerUIToolkit
    {
        public class DialogueEventHandler
        {
            private readonly DialogueListenerCollection _listeners;

            public DialogueEventHandler()
            {
                _listeners = new();
            }

            public void AddListener(IDialogueListener listener)
            {
                _listeners.AddListener(listener);
            }

            public void RemoveListener(IDialogueListener listener)
            {
                _listeners.RemoveListener(listener);
            }

            internal void RaiseBeginEvents(YamlDialogueStep firstStep)
            {
                _listeners.Invoke<IDialogueListener.IBeginDialogueListener>();
                RaiseStepEvents(firstStep, actorChanged: true, lineChanged: true);
            }

            internal void RaiseStepEvents(YamlDialogueStep step, bool actorChanged, bool lineChanged)
            {
                if (actorChanged)
                    _listeners.Invoke<IDialogueListener.IActorListener>(
                        step.Actor);

                if (lineChanged)
                    _listeners.Invoke<IDialogueListener.ILineListener>(
                        step.Line);

                if (step.Actions != null && step.Actions.Length > 0)
                    _listeners.Invoke<IDialogueListener.IActionsListener>(
                        step.Actions);

                if (step.Options != null && step.Options.Length > 0)
                    _listeners.Invoke<IDialogueListener.IOptionsListener>(
                        step.Options, step.ConfirmOption, step.CancelOption);

                _listeners.Invoke<IDialogueListener.IStepListener>(
                    step.Actor, step.Line, step.Actions, step.Options,
                    step.ConfirmOption, step.CancelOption);
            }

            internal void RaiseEndEvents()
            {
                _listeners.Invoke<IDialogueListener.IEndDialogueListener>();
            }
        }
    }
}
