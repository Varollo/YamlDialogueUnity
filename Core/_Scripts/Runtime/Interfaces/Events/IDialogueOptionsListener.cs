namespace YamlDialogueUnity
{
    public interface IDialogueOptionsListener
    {
        void OnPickOption(int option);
        void OnSelectOptionView(DialogueOptionViewBase view);
        void OnCancelOption();
    }
}