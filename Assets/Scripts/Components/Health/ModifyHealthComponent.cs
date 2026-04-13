using UnityEngine;

namespace Platformer.Components.Health
{

    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hpDelta = -1;

        public void SetDelta(int delta)
        {
            _hpDelta = delta;
        }

        public void Apply(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(_hpDelta);
            }

            if (healthComponent == null)
            {
                return;
            }

        }
    }

}