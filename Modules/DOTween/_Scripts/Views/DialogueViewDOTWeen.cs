using UnityEngine.UI;

namespace YamlDialogueUnity.DOTween
{
    public class DialogueViewDOTween : DialogueViewDOTweenBase<Text>
    {
        public override void UpdateActorTxt(string actorName) => Actor.text = actorName;
        public override void UpdateLineTxt(string line) => Line.text = line;
    }
}
