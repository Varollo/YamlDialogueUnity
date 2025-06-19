using UnityEngine;
using YamlDialogueUnity;

public class YamlDialogueExample01 : MonoBehaviour
{
    [SerializeField] private DialogueViewBase dialogueView;
    [SerializeField] private TextAsset dialogueFile;

    [ContextMenu("Play Dialogue")]
    private void Start()
    {
        dialogueView.Show(dialogueFile.text);
    }
}
