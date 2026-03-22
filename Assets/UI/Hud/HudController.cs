using Platformer.Model;
using Platformer.Model.Definitions;
using Platformer.Model.Definitions.Player;
using Platformer.UI.Widgets;
using Platformer.Utils;
using Platformer.Utils.Disposables;
using UnityEngine;

namespace Platformer.UI.Hud
{

    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidgets _healthBar;
        [SerializeField] private CurrentPerkWidget _currentPerk;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.Data.HP.SubscribeAndInvoke(OnHealthChanged));
            _trash.Retain(_session.PerksModel.Subscribe(OnPerkChanged));

            OnPerkChanged();
        }

        private void OnPerkChanged()
        {
            var usedPerkId = _session.PerksModel.Used;
            var hasPersk = !string.IsNullOrEmpty(usedPerkId);

            if (hasPersk)
            {
                var perkDef = (DefsFacade.I.Perks.Get(usedPerkId));
                _currentPerk.Set(perkDef);
            }

            _currentPerk.gameObject.SetActive(hasPersk);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {

            var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        public void OnSettings()
        {
            WindowUtils.CreateWindow("UI/InGameMainMenuWindow");
        }
        public void OnDebugus()
        {
            WindowUtils.CreateWindow("UI/PlayerStatsWindow");
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }

}