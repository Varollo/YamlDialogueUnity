using UnityEngine;
using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public abstract class DialogueActorViewBase : MonoBehaviour, IDialogueActorListener
    {
        [SerializeField] private Image[] actorImgs;
        
        private DialogueActorController _controller;

        private void Awake()
        {
            _controller = Initialize(actorImgs.Length);
        }

        protected Image GetActorImg(int id)
        {
            return actorImgs[id];
        }

        public void SetActor(ActorDatabaseSO database, string actorName, int maxActors)
        {
            Sprite actorSprite = database.GetActorSprite(actorName);

            if (actorSprite != null)
                _controller.SetActor(actorSprite, actorName, maxActors);
        }

        protected abstract DialogueActorController Initialize(int imgCount);
        public abstract void OnClearSlot(int slotId);
        public abstract void OnFillSlot(int slotId, Sprite actorSprite);
        public abstract void OnFocusSlot(int slotId);
        public abstract void OnUnfocusSlot(int slotId);
    }
}
