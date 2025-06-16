using UnityEngine;

namespace YamlDialogueUnity.Input
{
    public class LegacyInputManager : InputManagerBase
    {
        [SerializeField] private KeyCode _nextKeyCode = KeyCode.Return;

        public bool Enabled { get; private set; } = false;

        public override void Enable()
        {
            Enabled = true;
        }

        public override void Disable()
        {
            Enabled = false;
        }

        public override bool CheckInput()
        {
            return Enabled && UnityEngine.Input.GetKeyDown(_nextKeyCode);
        }
    }
}
