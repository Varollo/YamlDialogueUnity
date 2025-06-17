using UnityEngine;
using UnityEngine.EventSystems;

namespace YamlDialogueUnity
{
    public class DialogueInputHandler
    {
        public IDialogueInputTarget Selected { get; private set; }

        public void SelectTarget(IDialogueInputTarget target)
        {
            if (Selected == target)
                return;

            if (target is Component component)
                EventSystem.current.SetSelectedGameObject(component.gameObject);

            Selected?.OnDeselect();
            Selected = target;
            Selected?.OnSelect();
        }

    }
}