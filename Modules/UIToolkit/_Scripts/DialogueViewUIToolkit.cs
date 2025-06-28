using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace YamlDialogueUnity.UIToolkit
{
    [UxmlElement("DialogueView")]
    public partial class DialogueViewUIToolkit : VisualElement, 
        IDialogueListener.IBeginDialogueListener, IDialogueListener.IEndDialogueListener,
        IDialogueListener.IActorListener, IDialogueListener.ILineListener
    {
        public static readonly string ussClassName = "dialogue-view";
        public static readonly string lineClassName = "dialogue-view__line";
        public static readonly string actorClassName = "dialogue-view__actor";

        private readonly DialogueControllerUIToolkit _controller;

        private Label _lineLbl;
        private Label _actorLbl;

        public TextAsset _dialogueAsset;

        public DialogueViewUIToolkit()
        {
            AddToClassList(ussClassName);
            SetEnabled(false);

            _lineLbl = new Label();
            _lineLbl.AddToClassList(lineClassName);
            _lineLbl.SetEnabled(false);
            Add(_lineLbl);

            _actorLbl = new Label();
            _actorLbl.AddToClassList(actorClassName);
            _actorLbl.SetEnabled(false);
            Add(_actorLbl);

            RegisterCallback<ClickEvent>(evt => OnClick(evt));
            RegisterCallback<NavigationSubmitEvent>(evt => OnSubmit(evt));
            RegisterCallback<NavigationCancelEvent>(evt => OnCancel(evt));

            _controller = new();
            _controller.EventHandler.AddListener(this);
        }

        public void Show(TextAsset dialogueAsset)
        {
            _dialogueAsset = dialogueAsset;
            _controller.InitializeDialogue(dialogueAsset.text);
        }

        public void Next()
        {
            _controller.AdvanceDialogue();
        }

        #region Dialogue Listener Callbacks
        public void OnDialogueLine(string line)
        {
            _lineLbl.text = line;
        }

        public void OnDialogueActor(string actor)
        {
            _actorLbl.text = actor;

            bool hasActor = !string.IsNullOrEmpty(actor);

            _actorLbl.SetEnabled(hasActor);
            _lineLbl.SetEnabled(hasActor);
        }

        public void OnDialogueBegin()
        {
            SetEnabled(true);
        }

        public void OnDialogueEnd()
        {
            SetEnabled(false);
        }
        #endregion

        #region UI Callbacks
        private static void OnClick(ClickEvent evt)
        {
            var dialogueView = evt.currentTarget as DialogueViewUIToolkit;
            dialogueView.Next();

            evt.StopPropagation();
        }

        private static void OnSubmit(NavigationSubmitEvent evt)
        {
            var dialogueView = evt.currentTarget as DialogueViewUIToolkit;
            dialogueView.Next();

            evt.StopPropagation();
        }

        private static void OnCancel(NavigationCancelEvent evt)
        {
            var dialogueView = evt.currentTarget as DialogueViewUIToolkit;

            // TODO: select cancel option

            evt.StopPropagation();
        }
        #endregion
    }
}
