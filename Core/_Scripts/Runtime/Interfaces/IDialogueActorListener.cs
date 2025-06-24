using UnityEngine;

namespace YamlDialogueUnity
{
    public interface IDialogueActorListener
    {
        void OnClearSlot(int slotId);
        void OnFillSlot(int slotId, Sprite actorSprite);
        void OnFocusSlot(int slotId);
        void OnUnfocusSlot(int slotId);
    }
}