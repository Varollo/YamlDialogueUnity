using System.Collections.Generic;

namespace YamlDialogueUnity
{
    public class DialogueActionsHandler
    {
        private readonly Dictionary<string, List<IDialogueActionListener>> _listeners = new();

        public void AddListener(IDialogueActionListener listener, params string[] actions)
        {
            foreach (var action in actions)
            {
                if (_listeners.TryGetValue(action, out var listeners))
                    listeners.Add(listener);
                else
                    _listeners.Add(action, new() { listener });
            }
        }

        public void BroadcastActions(string[] actions)
        {
            for (int i = actions.Length - 1; i >= 0; i--)
            {
                var action = actions[i];

                if (_listeners.TryGetValue(action, out var listeners))
                {
                    for (int j = listeners.Count - 1; j >= 0; j--)
                    {
                        var listener = listeners[j];

                        if (listener != null)
                            listener.OnDialogueAction(action);
                        else
                            listeners.RemoveAt(j);
                    }
                }

                if (listeners != null && listeners.Count == 0)
                    _listeners.Remove(action);
            }
        }
    }
}
