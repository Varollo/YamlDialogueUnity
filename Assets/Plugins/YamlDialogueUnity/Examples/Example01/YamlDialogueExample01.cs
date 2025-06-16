using UnityEngine;
using YamlDialogueUnity.View;

public class YamlDialogueExample01 : MonoBehaviour
{
    [SerializeField] private DialogueView dialogueView;
    [SerializeField] private TextAsset dialogueFile;

    [ContextMenu("Play Dialogue")]
    private void Start()
    {
        dialogueView.Show(dialogueFile.text);
    }
}
