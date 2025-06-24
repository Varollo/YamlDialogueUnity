using System;

namespace YamlDialogueUnity
{
    public interface IEventSource<TListener> : IDisposable
    {
        void AddListener(TListener listener);
        void RemoveListener(TListener listener);
    }
}