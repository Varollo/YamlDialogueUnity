using UnityEngine;

namespace YamlDialogueUnity
{
    public class DialogueActorView : DialogueActorViewBase
    {
        protected override DialogueActorController CreateController(int imgCount, DialogueActionsHandler actionsHandler)
        {
            return new DialogueActorController(this, imgCount, actionsHandler);
        }

        public override void OnFocusSlot(int slotId)
        {
            GetActorImg(slotId).color = Color.white;
            GetActorImg(slotId).transform.localScale = Vector3.one;
        }

        public override void OnUnfocusSlot(int slotId)
        {
            GetActorImg(slotId).color = new Color(GetActorImg(slotId).color.r, GetActorImg(slotId).color.g, GetActorImg(slotId).color.b, .5f);
            GetActorImg(slotId).transform.localScale = new Vector3(.8f, .8f, .8f);
        }

        public override void OnFillSlot(int slotId, Sprite actorSprite)
        {
            GetActorImg(slotId).sprite = actorSprite;
            GetActorImg(slotId).enabled = actorSprite != null;
        }

        public override void OnClearSlot(int slotId)
        {
            GetActorImg(slotId).sprite = null;
            GetActorImg(slotId).enabled = false;
        }
    }
}
