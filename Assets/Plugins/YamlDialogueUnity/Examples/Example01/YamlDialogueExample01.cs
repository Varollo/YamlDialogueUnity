using TMPro;
using UnityEngine;
using YamlDialogueUnity;

public class YamlDialogueExample01 : MonoBehaviour
{
    [SerializeField] private DialogueViewBase dialogueView;
    [SerializeField] private TextAsset dialogueFile1, dialogueFile2;
    [SerializeField] private TMP_Text actionsTxt;

    [ContextMenu("Play Dialogue 1")]
    public void ShowDiaogue1()
    {
        dialogueView.Show(dialogueFile1.text);
    }

    [ContextMenu("Play Dialogue 2")]
    public void ShowDiaogue2()
    {
        dialogueView.Show(dialogueFile2.text);
    }

    public void OnDialogueStep(string actor, string line, string[] actions)
    {
        actionsTxt.text = actions != null && actions.Length > 0 
            ? string.Join("\n", actions) 
            : string.Empty;
    }
}
