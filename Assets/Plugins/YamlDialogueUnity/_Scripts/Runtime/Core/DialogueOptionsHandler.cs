using UnityEngine;
using System.Collections.Generic;

namespace YamlDialogueUnity
{
    public class DialogueOptionsHandler : IEventSource<IDialogueOptionsListener>
    {
        private readonly List<IDialogueOptionsListener> _listeners = new();
        private readonly List<DialogueOptionView> _optionViews = new();

        private readonly DialogueOptionView _optionPrefab;
        private readonly Transform _optionsHolder;
        
        private bool _isDisposed;

        public DialogueOptionsHandler(DialogueOptionView optionPrefab, Transform optionsHolder)
        {
            _optionPrefab = optionPrefab;
            _optionsHolder = optionsHolder;
        }

        public DialogueOptionView GetOptionView(int optionId)
        {
            return _optionViews[optionId];
        }

        public void CreateOptions(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
                _optionViews.Add(_optionPrefab.CreateInstance(
                    _optionsHolder, this, options[i], i));
        }

        public void ClearOptions()
        {
            for (int i = 0; i < _optionViews.Count; i++)
                _optionViews[i].Destroy();
            _optionViews.Clear();
        }

        public void PickOption(int id)
        {
            foreach(var listener in _listeners)
                listener.OnPickOption(id);
        }

        public void SelectCancelOption()
        {
            foreach (var listener in _listeners)
                listener.OnCancelOption();
        }

        public void AddListener(IDialogueOptionsListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IDialogueOptionsListener listener)
        {
            _listeners.Remove(listener);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _listeners.Clear();
                    _optionViews.Clear();
                }

                ClearOptions();

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}
