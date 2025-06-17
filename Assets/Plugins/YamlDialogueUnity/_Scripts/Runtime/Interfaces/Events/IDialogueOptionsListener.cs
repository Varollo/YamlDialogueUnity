namespace YamlDialogueUnity
{
    public interface IDialogueOptionsListener
    {
        void OnPickOption(int option);
        void OnCancelOption();
    }
}