using YamlDialogueLib;

namespace YamlDialogueUnity.UIToolkit
{
    public partial class DialogueControllerUIToolkit
    {               
        private YamlDialogue _dialogueInstance;

        public DialogueControllerUIToolkit()
        {
            EventHandler = new();
        }
        
        public DialogueEventHandler EventHandler { get; }

        public void InitializeDialogue(string text)
        {
            _dialogueInstance = YamlDialogueParser.Parse(text);            
            EventHandler.RaiseBeginEvents(_dialogueInstance.Current);
        }

        public void AdvanceDialogue()
        {
            var prevActor = _dialogueInstance.Current.Actor;
            var prevLine  = _dialogueInstance.Current.Line;

            if (_dialogueInstance.MoveNext())
                EventHandler.RaiseStepEvents(_dialogueInstance.Current,
                    actorChanged: _dialogueInstance.Current.Actor != prevActor,
                     lineChanged: _dialogueInstance.Current.Line  != prevLine);

            else EndDialogue();
        }

        public void EndDialogue()
        {
            _dialogueInstance = null;
            EventHandler.RaiseEndEvents();
        }
    }
}
