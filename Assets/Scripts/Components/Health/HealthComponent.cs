using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Components.Health
{

    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] public UnityEvent _onDie;
        [SerializeField] public HealthChangeEvent _onChange;

        public int Health => _health;

        public void ModifyHealth(int healthDelta)
        {
            if (_health <= 0) return;

            _health += healthDelta;
            _onChange?.Invoke(_health);

            if (healthDelta < 0)
            {
                _onDamage?.Invoke();
            }
            if(healthDelta > 0)
            {
                _onHeal?.Invoke();
            }
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        private void OnDestroy()
        {
            _onDie.RemoveAllListeners();
        }

        public void SetHealth(int health)
        {
           _health = health;
        }

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int> { }

    }

}