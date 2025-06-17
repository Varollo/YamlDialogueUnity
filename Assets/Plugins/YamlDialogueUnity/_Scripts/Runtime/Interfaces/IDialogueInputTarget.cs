using UnityEngine.EventSystems;

namespace YamlDialogueUnity
{
    public interface IDialogueInputTarget : ISubmitHandler, ICancelHandler 
    {
        void OnSelect();
        void OnDeselect();
    }
}