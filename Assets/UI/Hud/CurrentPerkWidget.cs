using Platformer.Model;
using Platformer.Model.Definitions.Repositories;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI.Hud
{
    public class CurrentPerkWidget:MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _cooldownImage;
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public void Set(PerkDef perkDef)
        {
            _icon.sprite = perkDef.Icon;
        }

        private void Update()
        {
            var cooldown = _session.PerksModel.Cooldown;
            _cooldownImage.fillAmount = cooldown.RemaningTime / cooldown.Value;
        }
    }
}
