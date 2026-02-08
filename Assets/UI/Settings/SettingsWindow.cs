using Platformer.Model.Data;
using Platformer.UI.Widgets;
using UnityEngine;

namespace Platformer.UI.Settings
{

    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidgets _music;
        [SerializeField] private AudioSettingsWidgets _sfx;
        protected override void Start()
        {
            base.Start();

            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }

    }
}
