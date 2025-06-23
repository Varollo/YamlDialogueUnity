using System;
using System.Collections.Generic;
using UnityEngine;

namespace YamlDialogueUnity
{
    public class DialogueActorController : IDialogueActionListener
    {
        private readonly Dictionary<string, int> _actorIdMap = new();
        private readonly IDialogueActorListener _listener;
        private readonly int _imgCount;

        public string[] _actorSlots;
        private int? _focusedSlot;
        private int _nextSlot;

        public DialogueActorController(IDialogueActorListener view, int imgCount, DialogueActionsHandler actionsHandler)
        {
            _listener = view;
            _imgCount = imgCount;

            _actorSlots = new string[_imgCount];
            
            actionsHandler.AddListener(this, "actorview_clear");
        }

        public void SetActor(Sprite actorSprite, string actorName, int maxActors)
        {            
            if (!ValidateMaxActors(maxActors))
                return;

            // No actor => Unfocus
            if (string.IsNullOrEmpty(actorName))
                UnfocusSlot();

            // Actor not slotted, or actor slot is filled => fill slot with actor
            else if (!_actorIdMap.TryGetValue(actorName, out var id)
            || (_actorSlots[id] != actorName))
                FillSlot(_nextSlot, actorName, actorSprite);

            // Actor is slotted but not focused => focus actor
            else if (id != _focusedSlot)
                FocusSlot(id);
        }

        private void FocusSlot(int slotId)
        {
            UnfocusSlot();
            _listener.OnFocusSlot(slotId);

            _nextSlot = (slotId + 1) % _actorSlots.Length;
            _focusedSlot = slotId;
        }

        private void UnfocusSlot()
        {
            if (!_focusedSlot.HasValue)
                return;

            _listener.OnUnfocusSlot(_focusedSlot.Value);
            _focusedSlot = null;
        }

        private void FillSlot(int slotId, string actorName, Sprite actorSprite)
        {
            if (!_actorIdMap.TryAdd(actorName, slotId))
                slotId = _actorIdMap[actorName];

            if (!string.IsNullOrEmpty(_actorSlots[slotId]))
                ClearSlot(slotId);

            if (_actorSlots[slotId] != actorName)
                _listener.OnFillSlot(slotId, actorSprite);
            
            _actorSlots[slotId] = actorName;
            FocusSlot(slotId);
        }

        private void ClearSlot(int slotId)
        {
            _actorSlots[slotId] = string.Empty;
            _listener.OnClearSlot(slotId);
        }

        private bool ValidateMaxActors(int maxActors)
        {
            if (maxActors > _imgCount)
            {
                Debug.LogError("YAML DIALOGUE UNITY ERROR: Not enough Image references to support {maxActors} actors at once.");
                return false;
            }

            if (maxActors < _actorSlots.Length)
                TrimSlots(maxActors);

            if (maxActors == 0)
                return false;

            return true;
        }

        private void TrimSlots(int maxActors)
        {
            for (int i = _actorSlots.Length - 1; i > maxActors; --i)
                ClearSlot(i);
            
            _nextSlot %= maxActors;
        }

        public void OnDialogueAction(string action)
        {
            if (action == "actorview_clear")
                for (int i = 0; i < _actorSlots.Length; i++)
                    ClearSlot(i);
        }

        public void Clear()
        {
            for (int i = _actorSlots.Length - 1; i >= 0; i--)
                ClearSlot(i);

            _actorIdMap.Clear();
        }
    }
}
