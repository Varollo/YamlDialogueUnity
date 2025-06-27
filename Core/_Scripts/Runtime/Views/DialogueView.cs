using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public class DialogueView : DialogueViewOldUI<Text>
    {
        protected override void SetLine(string line, Text lineTxt) => lineTxt.text = line;
        protected override void SetActorName(string actor, Text actorTxt) => actorTxt.text = actor;
    }
}
