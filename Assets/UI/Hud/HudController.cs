using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.UI.Widgets;
using Platformer.Utils;
using UnityEngine;

namespace Platformer.UI.Hud
{

    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidgets _healthBar;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.HP.OnChanged += OnHealthChanged;
            OnHealthChanged(_session.Data.HP.Value, 0);
        }
        
        private void OnHealthChanged(int newValue, int oldValue)
        {

            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        public void OnSettings()
        {
            WindowUtils.CreateWindow("UI/InGameMainMenuWindow");
        }

        private void OnDestroy()
        {
            _session.Data.HP.OnChanged -= OnHealthChanged;
        }
    }

}