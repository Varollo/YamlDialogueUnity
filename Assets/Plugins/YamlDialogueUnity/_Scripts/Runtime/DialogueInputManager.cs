using System;
using UnityEngine;

namespace YamlDialogueUnity
{
    [RequireComponent(typeof(DialogueView))]
    public class DialogueInputManager : MonoBehaviour
    {
        [SerializeField] private KeyCode nextKey = KeyCode.Return;

        private DialogueView _view;

        private void Awake()
        {
            _view = GetComponent<DialogueView>();
        }

        public void Enable()
        {
            enabled = true;
        }

        internal void Disable()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(nextKey))
                _view.Next();
        }
    }
}
