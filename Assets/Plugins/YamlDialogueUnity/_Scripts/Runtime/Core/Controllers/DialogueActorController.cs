using System.Collections.Generic;
using UnityEngine;

namespace YamlDialogueUnity
{
    public class DialogueActorController
    {
        private readonly Dictionary<string, int> _actorIdMap = new();
        private readonly IDialogueActorListener _listener;
        private readonly int _imgCount;

        private int _nextSlot;

        public DialogueActorController(IDialogueActorListener view, int imgCount)
        {
            _listener = view;
            _imgCount = imgCount;

            ActorSlots = new string[_imgCount];
        }

        public string[] ActorSlots { get; private set; }

        private bool IsFree(int slotId) => string.IsNullOrEmpty(ActorSlots[slotId]);

        public void SetActor(Sprite actorSprite, string actorName, int maxActors)
        {            
            if (!ValidateSetActor(actorName, maxActors, actorSprite))
                return;

            if (_actorIdMap.TryGetValue(actorName, out var id))
                ActivateSlot(id);
            else
                FillSlot(_nextSlot, actorName, actorSprite);
        }

        private void ActivateSlot(int slotId)
        {
            _nextSlot = (slotId + 1) % ActorSlots.Length;
            _listener.OnFocusSlot(slotId);

            for (int i = 0; i < ActorSlots.Length; i++)
                if (i != slotId && !IsFree(i))
                    _listener.OnUnfocusSlot(i);
        }

        private void FillSlot(int slotId, string actorName, Sprite actorSprite)
        {
            if (!IsFree(slotId))
                ClearSlot(slotId);

            _actorIdMap.Add(actorName, slotId);
            ActorSlots[slotId] = actorName;
            
            _listener.OnFillSlot(slotId, actorSprite);
            ActivateSlot(slotId);
        }

        private void ClearSlot(int slotId)
        {
            if (IsFree(slotId))
                return;

            _actorIdMap.Remove(ActorSlots[slotId]);
            ActorSlots[slotId] = string.Empty;
            
            _listener.OnClearSlot(slotId);
        }

        private bool ValidateSetActor(string actorName, int maxActors, Sprite actorSprite)
        {
            return ValidateActorName(actorName)
                && ValidateMaxActors(maxActors)
                && ValidateActorSprite(actorSprite);
        }

        private bool ValidateMaxActors(int maxActors)
        {
            if (maxActors > _imgCount)
            {
                Debug.LogError("YAML DIALOGUE UNITY ERROR: Not enough Image references to support {maxActors} actors at once.");
                return false;
            }

            if (maxActors < ActorSlots.Length)
            {
                TrimSlots(maxActors);

                if (maxActors == 0)
                    return false;
            }

            return true;
        }

        private static bool ValidateActorSprite(Sprite actorSprite)
        {
            return actorSprite != null;
        }

        private static bool ValidateActorName(string actorName)
        {
            return !string.IsNullOrEmpty(actorName);
        }

        private void TrimSlots(int maxActors)
        {
            for (int i = maxActors; i < ActorSlots.Length; ++i)
                ClearSlot(i);
            
            _nextSlot %= maxActors;
        }
    }
}
