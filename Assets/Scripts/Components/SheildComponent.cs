using Platformer.Components.Health;
using Platformer.Utils;
using UnityEngine;

namespace Platformer.Components
{
    public class SheildComponent:MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Cooldown _cooldown;

        public void  Use()
        {
            _health.Immune = true;
            _cooldown.Reset();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_cooldown.IsReady)
                gameObject.SetActive(false);
        }
        
        private void OnDisable()
        {
            _health.Immune = false;
        }

    }
}
