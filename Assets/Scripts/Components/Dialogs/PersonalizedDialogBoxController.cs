using Platformer.Model.Data;
using Platformer.UI.Hud.Dialogs;
using UnityEngine;

namespace Platformer.Components.Dialogs
{
    public class PersonalizedDialogBoxController : DialogBoxController
    {
        [SerializeField] private DialogContent _right;

        protected override DialogContent CurrentContent => CurrentSentence.Side == Side.Left ? _content : _right;

        protected override void OnStartDialogAnimation()
        {
            _content.gameObject.SetActive(CurrentSentence.Side == Side.Left);
            _right.gameObject.SetActive(CurrentSentence.Side == Side.Right);

            base.OnStartDialogAnimation();
        }

    }
}
