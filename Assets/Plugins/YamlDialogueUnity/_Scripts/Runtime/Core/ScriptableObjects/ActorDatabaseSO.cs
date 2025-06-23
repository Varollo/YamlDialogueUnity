using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YamlDialogueUnity
{
    [CreateAssetMenu(fileName = "Actor Image Database", menuName = "YAMLDialogueUnity/Actor Image Database")]
    public class ActorDatabaseSO : ScriptableObject
    {
        [SerializeField] private ActorData[] actorData;

        private Dictionary<string, Sprite> _actorNameSpriteMap;

        public Sprite GetActorSprite(string actorName)
        {
            if (string.IsNullOrEmpty(actorName))
                return null;
            
            return (_actorNameSpriteMap ??= actorData
                .ToDictionary(a => a.Name, a => a.Sprite))
                .TryGetValue(actorName, out var sprite) ? sprite : null;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _actorNameSpriteMap = null;
        }
#endif

        [System.Serializable]
        private struct ActorData
        {
            public string Name;
            public Sprite Sprite;
        }
    }
}
