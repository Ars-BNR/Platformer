using Platformer.Model;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace Platformer.Creatures.Hero
{
    public class HeroFlashlight : MonoBehaviour
    {
        [SerializeField] private float _consumePerSecond;
        [SerializeField] private Light2D _light;

        private GameSession _session;
        private float _defaultIntensity;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _defaultIntensity = _light.intensity;
        }
        private void Update()
        {
            var consumed = Time.deltaTime * _consumePerSecond;
            var currentValue = _session.Data.Fuel.Value;
            var nextValue = currentValue - consumed;
            nextValue = Mathf.Max(nextValue, 0);
            _session.Data.Fuel.Value = nextValue;

            var progress = nextValue / 100;
            _light.intensity = _defaultIntensity * progress;
        }

    }
}
