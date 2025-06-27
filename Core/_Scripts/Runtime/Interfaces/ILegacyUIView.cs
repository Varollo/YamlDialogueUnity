using UnityEngine;
using UnityEngine.UI;

namespace YamlDialogueUnity
{
    public interface ILegacyUIView
    {
        CanvasGroup CanvasGroup { get; }
        Graphic ActorName { get; }
        Graphic Line { get; }
    }
}
