using Platformer.Components.Health;
using Platformer.Utils;
using Platformer.Utils.Disposables;
using UnityEngine;

namespace Platformer.UI.Widgets
{
    public class BossHPWidget : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private ProgressBarWidgets _hpBar;
        [SerializeField] private CanvasGroup _canvas;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private float _maxHealth;

        private void Start()
        {
            _maxHealth = _health.Health;
            _trash.Retain(_health._onChange.Subsrcibe(OnHpChanged));
            _trash.Retain(_health._onDie.Subsrcibe(HideUI));
        }

        [ContextMenu("Show")]
        public void ShowUI()
        {
            this.LerpAnimated(0, 1, 1, SetAlpha);
        }
        private void SetAlpha(float alpha)
        {
            _canvas.alpha = alpha;
        }

        [ContextMenu("Hide")]
        private void HideUI()
        {
            this.LerpAnimated(1, 0, 1, SetAlpha);
        }

        private void OnHpChanged(int hp)
        {
            _hpBar.SetProgress(hp / _maxHealth);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
