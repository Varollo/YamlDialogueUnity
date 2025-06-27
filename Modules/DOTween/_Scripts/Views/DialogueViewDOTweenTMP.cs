using TMPro;

namespace YamlDialogueUnity.DOTween
{
    public class DialogueViewDOTweenTMP : DialogueViewDOTweenBase<TMP_Text>
    {
        public override void UpdateActorTxt(string actorName) => Actor.text = actorName;
        public override void UpdateLineTxt(string line) => Line.text = line;
    }
}
