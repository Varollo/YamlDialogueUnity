using System;
using UnityEngine.EventSystems;

namespace YamlDialogueUnity
{
    public class ViewSelectionHandler : IDisposable
    {
        public SelectableView Selected { get; private set; }

        public void SelectView(SelectableView view)
        {
            if (Selected != null)
                Selected.Deselect();

            Selected = view;

            if (Selected != null)
                Selected.Select();
        }

        public void Dispose()
        {
            if (Selected != null)
                Selected.Deselect();
        }
    }
}
