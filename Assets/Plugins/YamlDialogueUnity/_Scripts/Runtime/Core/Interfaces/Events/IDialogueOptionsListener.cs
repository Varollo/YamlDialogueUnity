namespace YamlDialogueUnity
{
    public interface IDialogueOptionsListener
    {
        void OnPickOption(int option);
        void OnSelectOptionView(DialogueOptionView view);
        void OnCancelOption();
    }
}