using Platformer.Utils;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI.MainMenu
{

    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;
        public void OnShowSettings()
        {
           WindowUtils.CreateWindow("UI/SettingsWindow");
        }
        public void OnLanguages()
        {
           WindowUtils.CreateWindow("UI/LocalizationWindow");
        }

        public void OnStartGame()
        {
            _closeAction = () =>
            {
                SceneManager.LoadScene("Level_1");
            };
            Close();
        }

        public void OnExit()
        {
            _closeAction = () =>
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }
    }

}