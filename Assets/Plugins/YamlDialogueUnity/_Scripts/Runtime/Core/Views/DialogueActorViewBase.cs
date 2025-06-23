using System;
using UnityEngine;
using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public abstract class DialogueActorViewBase : MonoBehaviour, IDialogueActorListener
    {
        [SerializeField] private Image[] actorImgs;
        
        private DialogueActorController _controller;

        public void Initialize(DialogueActionsHandler actionsHandler)
        {
            _controller = CreateController(actorImgs.Length, actionsHandler);
            
        }

        protected Image GetActorImg(int id)
        {
            return actorImgs[id];
        }

        public void SetActor(ActorDatabaseSO database, string actorName, int maxActors)
        {
            Sprite actorSprite = database.GetActorSprite(actorName);
            _controller.SetActor(actorSprite, actorName, maxActors);
        }

        public void Clear()
        {
            _controller.Clear();
        }

        protected abstract DialogueActorController CreateController(int imgCount, DialogueActionsHandler actionsHandler);
        public abstract void OnClearSlot(int slotId);
        public abstract void OnFillSlot(int slotId, Sprite actorSprite);
        public abstract void OnFocusSlot(int slotId);
        public abstract void OnUnfocusSlot(int slotId);
    }
}
