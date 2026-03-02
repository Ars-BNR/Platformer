using Platformer.Components.Health;
using Platformer.Utils.Disposables;
using System;
using UnityEngine;

namespace Platformer.UI.Widgets
{

    public class LifeBarWidgets : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidgets _lifeBar;
        [SerializeField] private HealthComponent _hp;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private int _maxHP;

        private void Start ()
        {
            if (_hp == null)
                _hp.GetComponent<HealthComponent>();

            _maxHP = _hp.Health;

            _trash.Retain(_hp._onDie.Subsrcibe(OnDie));
            _trash.Retain(_hp._onChange.Subscibe(OnHPChanged));
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHPChanged(int hp)
        {
            var progress = (float) hp / _maxHP;
            _lifeBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }

}