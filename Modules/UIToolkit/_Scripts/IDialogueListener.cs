namespace YamlDialogueUnity.UIToolkit
{
    public interface IDialogueListener
    {
        public interface IStepListener : IDialogueListener
        {
            void OnDialogueStep(string actor, string line, string[] actions, string[] options, int confirmOptioniD, int cancelOptionId);
        }

        public interface IActorListener : IDialogueListener
        {
            void OnDialogueActor(string actor);
        }

        public interface ILineListener : IDialogueListener
        {
            void OnDialogueLine(string line);
        }

        public interface IOptionsListener : IDialogueListener
        {
            void OnDialogueOptions(string[] options, int confirmOptionId, int cancelOptionId);
        }

        public interface IActionsListener : IDialogueListener
        {
            void OnDialogueActions(string[] actions);
        }

        public interface IBeginDialogueListener : IDialogueListener
        {
            void OnDialogueBegin();
        }

        public interface IEndDialogueListener : IDialogueListener
        {
            void OnDialogueEnd();
        }
    }
}
