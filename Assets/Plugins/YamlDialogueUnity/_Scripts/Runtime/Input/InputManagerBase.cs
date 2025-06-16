using UnityEngine;

namespace YamlDialogueUnity.Input
{
    public abstract class InputManagerBase : MonoBehaviour
    {
        public abstract bool CheckInput();
        public abstract void Disable();
        public abstract void Enable();
    }
}