using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YamlDialogueUnity.UIToolkit
{

    public partial class DialogueControllerUIToolkit
    {
        public class DialogueListenerCollection : IEnumerable<IDialogueListener>
        {
            private HashSet<IDialogueListener> _listeners;
            private Dictionary<Type, HashSet<object>> _listenersTypeMap;

            public DialogueListenerCollection() : this(null) { }
            public DialogueListenerCollection(IEnumerable<IDialogueListener> listeners)
            {
                _listeners = new();
                _listenersTypeMap = typeof(IDialogueListener).GetNestedTypes()
                    .ToDictionary(t => t, _ => new HashSet<object>());

                if (listeners != null)
                    foreach(var listener in listeners)
                        AddListener(listener);
            }

            public void Invoke<TListener>(params object[] args) where TListener : IDialogueListener
            {
                foreach(var iface in typeof(TListener).GetInterfaces().Union(new Type[] { typeof(TListener) }))
                    if (_listenersTypeMap.TryGetValue(iface, out var typeSet))
                        foreach (var listener in typeSet)
                            iface.GetMethods()[0].Invoke(listener, args);
            }

            public void AddListener<TListener>(TListener listener) where TListener : IDialogueListener
            {
                FilterSetsByListener(listener,
                    typeSet => typeSet.Add(listener));

                _listeners.Add(listener);
            }

            public bool RemoveListener<TListener>(TListener listener) where TListener : IDialogueListener
            {
                if (!_listeners.Remove(listener))
                    return false;

                FilterSetsByListener(listener, 
                    typeSet => typeSet.Remove(listener));
                
                return true;
            }

            private void FilterSetsByListener<TListener>(TListener listener, Action<HashSet<object>> onSet) where TListener : IDialogueListener
            {
                FilterSetsByType(listener.GetType(), onSet);
            }

            private void FilterSetsByType(Type type, Action<HashSet<object>> onSet)
            {                
                foreach(var iface in type.GetInterfaces())
                    if (_listenersTypeMap.TryGetValue(iface, out var typeSet))
                        onSet.Invoke(typeSet);
            }

            public IEnumerator<IDialogueListener> GetEnumerator()
            {
                return ((IEnumerable<IDialogueListener>)_listeners).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)_listeners).GetEnumerator();
            }
        }
    }
}
