using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public abstract class SelectableView : MonoBehaviour,
        ISubmitHandler, ICancelHandler, IPointerClickHandler
    {
        public bool IsSelected { get; private set; }

        protected virtual void Awake()
        {
            if (!GetComponent<Selectable>())
                gameObject.AddComponent<Selectable>().hideFlags = HideFlags.HideInInspector;
        }

        public void Select()
        {
            if (IsSelected)
                return;
            
            IsSelected = true;

            if (EventSystem.current.currentSelectedGameObject != this
            && !EventSystem.current.alreadySelecting)
                EventSystem.current.SetSelectedGameObject(gameObject);

            OnSelect();
        }

        public void Deselect()
        {
            if (!IsSelected)
                return;

            IsSelected = false;

            OnDeselect();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (IsSelected)
                OnSubmit();
        }

        public void OnCancel(BaseEventData eventData)
        {
            if (IsSelected)
                OnCancel();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnSubmit(eventData);
        }

        public virtual void OnSelect() { }
        public virtual void OnDeselect() { }
        public virtual void OnSubmit() { }
        public virtual void OnCancel() { }
    }
}
